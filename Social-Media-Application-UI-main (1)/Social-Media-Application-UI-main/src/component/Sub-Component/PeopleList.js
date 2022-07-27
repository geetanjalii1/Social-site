import React from "react";
import "./PeopleList.scss";
import HomePageService from "../../services/HomePageService";

import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import Button from "@material-ui/core/Button";
import GroupAddIcon from "@material-ui/icons/GroupAdd";
import CancelIcon from "@material-ui/icons/Cancel";

const homePageService = new HomePageService();

export default function PeopleList(props) {
  const SendFriendRequest = (ToUserID) => {
    let data = {
      fromUserID: localStorage.getItem("UserID"),
      toUserID: ToUserID,
    };
    homePageService
      .SendFriendRequest(data)
      .then((data) => {
        console.log("SendFriendRequest Data : ", data);
        props.GetAllUserList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
      })
      .catch((error) => {
        console.log("SendFriendRequest Error : ", error);
        props.GetAllUserList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
      });
  };

  const CancleFriendRequest = (FriendRequestID) => {
    console.log(
      "Cancle Friend Request Calling... FriendRequestID : ",
      FriendRequestID
    );
    homePageService
      .CancleFriendRequest(FriendRequestID)
      .then((data) => {
        console.log("CancleFriendRequest Data : ", data);
        props.GetAllUserList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
      })
      .catch((error) => {
        console.log("CancleFriendRequest Error : ", error);
        props.GetAllUserList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
      });
  };

  return (
    <div className="PeopleList-Container">
      {Array.isArray(props.PeopleList) && props.PeopleList.length > 0
        ? props.PeopleList.map(function (data, index) {
            return (
              <div className="People-data-flex" key={index}>
                <div className="Image">
                  <AccountCircleIcon fontSize="large" />
                </div>
                <div className="Friend-Name">{data.toUserName}</div>
                <div className="Unfriend-Button">
                  <Button
                    variant="contained"
                    style={{
                      backgroundColor: "black",
                      color: "white",
                      width: 40,
                      fontSize: 12,
                      textTransform: "none",
                    }}
                    onClick={() => {
                      SendFriendRequest(data.toUserID);
                    }}
                  >
                    <GroupAddIcon />
                  </Button>
                </div>
              </div>
            );
          })
        : null}
      {Array.isArray(props.SendRequestList) && props.SendRequestList.length > 0
        ? props.SendRequestList.map(function (data, index) {
            console.log("Send Request List Data ", data);
            return (
              <div className="People-data-flex" key={index}>
                <div className="Image">
                  <AccountCircleIcon fontSize="large" />
                </div>
                <div className="Friend-Name">{data.fullName}</div>
                <div className="Unfriend-Button">
                  <Button
                    variant="contained"
                    style={{
                      backgroundColor: "black",
                      color: "white",
                      width: 40,
                      fontSize: 12,
                      textTransform: "none",
                    }}
                    onClick={() => {
                      CancleFriendRequest(data.friendRequestID);
                    }}
                  >
                    <CancelIcon />
                  </Button>
                </div>
              </div>
            );
          })
        : null}
    </div>
  );
}
