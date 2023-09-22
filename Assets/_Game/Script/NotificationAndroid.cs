using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
// lỗi notify không click được là do firebase messaging trong android manifest
public class NotificationAndroid : MonoBehaviour
{
    private static bool initialized;
    string channelID = "channel_id";
    private AndroidNotificationChannel notificationChannel;
    void Start()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;
        // if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        // {
        //     Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        // }

        notificationChannel = new AndroidNotificationChannel()
        {
            Id = channelID,
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic Notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        StartCoroutine(RequestNotificationPermission());
    }
    IEnumerator RequestNotificationPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;
        // here use request.Status to determine users response
    }


    private void OnApplicationQuit()
    {
        SendMultipleNotis();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SendMultipleNotis();
        }
    }

    void SendMultipleNotis()
    {
        string title = "Antistress Relaxation Toys";
        string content1 = "Don't miss today's relaxation games";
        string content2 = "It's time to relax, let's play now!";
        string content3 = "Discover new exciting games and toys!";
        string content4 = "Have a nice day!";
        //  string content5 = "Let unlock and try our funniest games";
        string content6 = "Try our latest relaxation toys";
        SendNoti(1, System.DateTime.Now.AddDays(1), title, content1);
        SendNoti(1, System.DateTime.Now.AddDays(3), title, content2);
        SendNoti(1, System.DateTime.Now.AddDays(5), title, content6);
        SendNoti(1, System.DateTime.Now.AddDays(7), title, content3);
        SendNoti(1, System.DateTime.Now.AddDays(14), title, content4);
    }

    void SendNoti(int id, DateTime schedule, string title, string content)
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = content;
        notification.SmallIcon = "icon_1";
        notification.LargeIcon = "icon_2";
        notification.ShowTimestamp = true;
        notification.FireTime = schedule;

        var identifier = AndroidNotificationCenter.SendNotification(notification, channelID);
        //
        // if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        // {
        //     AndroidNotificationCenter.CancelAllNotifications();
        //     AndroidNotificationCenter.SendNotification(notification, sId);
        // }
        var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier);

        if (notificationStatus == NotificationStatus.Scheduled)
        {
            // Replace the scheduled notification with a new notification.
            AndroidNotificationCenter.UpdateScheduledNotification(id, notification, channelID);
        }
        else if (notificationStatus == NotificationStatus.Delivered)
        {
            // Remove the previously shown notification from the status bar.
            AndroidNotificationCenter.CancelNotification(id);
        }
        else if (notificationStatus == NotificationStatus.Unknown)
        {
            AndroidNotificationCenter.SendNotification(notification, channelID);
        }
    }
}