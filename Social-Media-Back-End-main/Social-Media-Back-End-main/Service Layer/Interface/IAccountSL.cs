using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Interface
{
    public interface IAccountSL
    {
        
        public Task<GetAllFriendListResponse> GetAllFriendList(int UserID);
        public Task<GetAllUserListResponse> GetAllUserList(int UserID);
        public Task<SendFriendRequestResponse> SendFriendRequest(SendFriendRequestRequest request);
        public Task<AcceptFriendRequestResponse> AcceptFriendRequest(AcceptFriendRequestRequest request);
        public Task<GetAllFriendRequestListResponse> GetAllFriendRequestList(int UserID);
        public Task<RejectFriendRequestResponse> RejectFriendRequest(int request);
        public Task<GetSendFriendRequestListResponse> GetSendFriendRequestList(int UserID);
        public Task<CancleFriendRequestResponse> CancleFriendRequest(int FriendRequestID);
        public Task<SearchPeopleResponse> SearchPeople(SearchPeopleRequest request);
        //Post
        public Task<InsertPostResponse> InsertPost(InsertPostRequest request);
        public Task<GetPostResponse> GetPost();
        public Task<DeletePostResponse> DeletePost(int PostID);  
    }
}
