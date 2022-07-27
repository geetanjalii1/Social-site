using Common_Layer.Models;
using Repository_Layer.Interface;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Sercvice
{
    public class AccountSL: IAccountSL
    {
        private readonly IAccountRL _accountRL;
        public AccountSL(IAccountRL accountRL)
        {
            _accountRL = accountRL;
        }

        public async Task<AcceptFriendRequestResponse> AcceptFriendRequest(AcceptFriendRequestRequest request)
        {
            return await _accountRL.AcceptFriendRequest(request);
        }

        public async Task<CancleFriendRequestResponse> CancleFriendRequest(int FriendRequestID)
        {
            return await _accountRL.CancleFriendRequest(FriendRequestID);
        }

        public async Task<DeletePostResponse> DeletePost(int PostID)
        {
            return await _accountRL.DeletePost(PostID);
        }

        public async Task<GetAllFriendListResponse> GetAllFriendList(int UserID)
        {
            return await _accountRL.GetAllFriendList(UserID);
        }

        public async Task<GetAllFriendRequestListResponse> GetAllFriendRequestList(int UserID)
        {
            return await _accountRL.GetAllFriendRequestList(UserID);
        }

        public async Task<GetAllUserListResponse> GetAllUserList(int UserID)
        {
            return await _accountRL.GetAllUserList(UserID);
        }

        public async Task<GetPostResponse> GetPost()
        {
            return await _accountRL.GetPost();
        }

        public async Task<GetSendFriendRequestListResponse> GetSendFriendRequestList(int UserID)
        {
            return await _accountRL.GetSendFriendRequestList(UserID);
        }

        public async Task<InsertPostResponse> InsertPost(InsertPostRequest request)
        {
            return await _accountRL.InsertPost(request);
        }

        public async Task<RejectFriendRequestResponse> RejectFriendRequest(int request)
        {
            return await _accountRL.RejectFriendRequest(request);
        }

        public async Task<SearchPeopleResponse> SearchPeople(SearchPeopleRequest request)
        {
            return await _accountRL.SearchPeople(request);
        }

        public async Task<SendFriendRequestResponse> SendFriendRequest(SendFriendRequestRequest request)
        {
            return await _accountRL.SendFriendRequest(request);
        }
    }
}
