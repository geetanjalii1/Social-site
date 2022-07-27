const ParentConfiguration = require("./ParentConfiguration");

module.exports = {
  GetAllFriendList: ParentConfiguration.Parent + "api/Account/GetAllFriendList?UserID=",
  GetAllUserList: ParentConfiguration.Parent + "api/Account/GetAllUserList?UserID=",
  SendFriendRequest: ParentConfiguration.Parent + "api/Account/SendFriendRequest",
  GetAllFriendRequestList: ParentConfiguration.Parent + "api/Account/GetAllFriendRequestList?UserID=",
  RejectFriendRequest: ParentConfiguration.Parent + "api/Account/RejectFriendRequest?FriendRequestID=",
  GetSendFriendRequestList:ParentConfiguration.Parent + "api/Account/GetSendFriendRequestList?UserID=",
  AcceptFriendRequest:ParentConfiguration.Parent + "api/Account/AcceptFriendRequest",
  CancleFriendRequest:ParentConfiguration.Parent + "api/Account/CancleFriendRequest?FriendRequestID=",
  SearchPeople:ParentConfiguration.Parent + "api/Account/SearchPeople",
  InsertPost:ParentConfiguration.Parent + "api/Account/InsertPost",
  GetPost:ParentConfiguration.Parent + "api/Account/GetPost",
  DeletePost:ParentConfiguration.Parent + "api/Account/DeletePost",
};
