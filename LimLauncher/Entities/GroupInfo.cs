﻿using System.Collections.ObjectModel;

namespace LimLauncher.Entities;

[PropertyChanged.AddINotifyPropertyChangedInterface]
public class GroupInfo
{
    public string ID { get; set; } = Guid.NewGuid().ToString();

    private string _groupName = string.Empty;
    public string GroupName
    {
        get { return _groupName; }
        set
        {
            _groupName = value;
            RenameGroupNameToDB();
        }
    }

    public ObservableCollection<ShortcutInfo> ListShortcutInfo { get; set; } = new ObservableCollection<ShortcutInfo>();

    public void LoadShortcuts()
    {
        foreach (var shortinfo in SqliteHelper.Instance.ExecuteQuery<ShortcutInfo>($"Select * from ShortcutInfo Where GroupID = '{this.ID}'"))
        {
            ListShortcutInfo.Add(shortinfo);
        }
    }
    public void AddNewGroupToDB()
    {
        SqliteHelper.Instance.ExecuteNonQuery(
            "insert into GroupInfo (ID,GroupName) values (@Id,@GroupName)",
            new Dictionary<string, object>()
            {
                {"Id", this.ID},
                {"GroupName",this.GroupName }
            });
    }

    public void RemoveGroupFromDB()
    {
        SqliteHelper.Instance.ExecuteNonQuery(
            "Delete from GroupInfo where Id=@Id",
            new Dictionary<string, object>()
            {
                {"Id", this.ID}
            });
        SqliteHelper.Instance.ExecuteNonQuery(
            "Delete from ShortcutInfo where GroupID=@Id",
            new Dictionary<string, object>()
            {
                {"Id", this.ID}
            });
    }

    private void RenameGroupNameToDB()
    {
        SqliteHelper.Instance.ExecuteNonQuery(
            "update GroupInfo set GroupName=@GroupName where ID=@Id",
            new Dictionary<string, object>()
            {
                {"GroupName", this.GroupName},
                {"Id", this.ID}
            });
    }
}
