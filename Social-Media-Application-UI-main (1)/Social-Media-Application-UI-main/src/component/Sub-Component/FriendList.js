import React, { useEffect, useState } from "react";
import "./FriendList.scss";
import HomePageService from "../../services/HomePageService";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import Button from "@material-ui/core/Button";
import CancelIcon from "@material-ui/icons/Cancel";

const homePageService = new HomePageService();

export default function FriendList(props) {
  const [UserName, setUserName] = useState("");

  useEffect(() => {
    setUserName(localStorage.getItem("FullName"));
  }, []);

  const AcceptFriendRequest = (FriendRequestID) => {
    let data = {
      friendRequestID: FriendRequestID.toString(),
    };
    homePageService
      .AcceptFriendRequest(data)
      .then((data) => {
        console.log("AcceptFriendRequest Data : ", data);
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
        props.GetAllFriendList(localStorage.getItem("UserID"));
      })
      .catch((error) => {
        console.log("AcceptFriendRequest Error : ", error);
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
        props.GetAllFriendList(localStorage.getItem("UserID"));
      });
  };

  const RejectFriendRequest = (FriendRequestID) => {
    homePageService
      .RejectFriendRequest(FriendRequestID)
      .then((data) => {
        console.log("RejectFriendRequest Data : ", data);
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
      })
      .catch((error) => {
        console.log("RejectFriendRequest Error : ", error);
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
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
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
        props.GetAllFriendList(localStorage.getItem("UserID"));
      })
      .catch((error) => {
        console.log("CancleFriendRequest Error : ", error);
        props.GetAllUserList(localStorage.getItem("UserID"));
        props.GetSendFriendRequestList(localStorage.getItem("UserID"));
        props.GetAllFriendRequestList(localStorage.getItem("UserID"));
        props.GetAllFriendList(localStorage.getItem("UserID"));
      });
  };

  return (
    <div className="FrindList-Container">
      {Array.isArray(props.FriendRequest) && props.FriendRequest.length > 0
        ? props.FriendRequest.map(function (data, index) {
            console.log("Friend Request Data : ", data);
            return (
              <div className="Friend-data-flex" key={index}>
                <div className="Image">
                  <AccountCircleIcon fontSize="large" />
                </div>
                <div className="Friend-Name">{data.fullName}</div>
                <div className="Unfriend-Button" style={{ display: "flex" }}>
                  <Button
                    variant="contained"
                    style={{
                      backgroundColor: "black",
                      color: "white",
                      width: 60,
                      fontSize: 12,
                      textTransform: "none",
                    }}
                    onClick={() => {
                      AcceptFriendRequest(data.friendRequestID);
                    }}
                  >
                    Accept
                  </Button>
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
      {Array.isArray(props.FriendList) && props.FriendList.length > 0
        ? props.FriendList.map(function (data, index) {
            return (
              <div className="Friend-data-flex" key={index}>
                <div className="Image">
                  <AccountCircleIcon fontSize="large" />
                </div>
                <div className="Friend-Name">
                  {data.fromFullName === UserName
                    ? data.toFullName
                    : data.toFullName === UserName
                    ? data.fromFullName
                    : null}
                </div>
                <div className="Unfriend-Button">
                  <Button
                    variant="contained"
                    style={{
                      backgroundColor: "black",
                      color: "white",
                      width: 80,
                      fontSize: 12,
                      textTransform: "none",
                    }}
                    onClick={() => {
                      CancleFriendRequest(data.friendRequestID);
                    }}
                  >
                    UnFriend
                  </Button>
                </div>
              </div>
            );
          })
        : null}
    </div>
  );
}
