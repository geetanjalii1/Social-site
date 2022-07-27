using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media_Application_BE.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountSL _accountSL;
        public AccountController(IAccountSL accountSL)
        {
            _accountSL = accountSL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriendList([FromQuery] int UserID)
        {
            GetAllFriendListResponse response = new GetAllFriendListResponse();
            try
            {
                response = await _accountSL.GetAllFriendList(UserID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserList([FromQuery] int UserID)
        {
            GetAllUserListResponse response = new GetAllUserListResponse();
            try
            {
                response = await _accountSL.GetAllUserList(UserID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriendRequestList([FromQuery] int UserID)
        {
            GetAllFriendRequestListResponse response = new GetAllFriendRequestListResponse();
            try
            {
                response = await _accountSL.GetAllFriendRequestList(UserID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(SendFriendRequestRequest request)
        {
            SendFriendRequestResponse response = new SendFriendRequestResponse();
            try
            {
                response = await _accountSL.SendFriendRequest(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> RejectFriendRequest([FromQuery] int FriendRequestID)
        {
            RejectFriendRequestResponse response = new RejectFriendRequestResponse();
            try
            {
                response = await _accountSL.RejectFriendRequest(FriendRequestID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetSendFriendRequestList([FromQuery] int UserID)
        {
            GetSendFriendRequestListResponse response = new GetSendFriendRequestListResponse();
            try
            {
                response = await _accountSL.GetSendFriendRequestList(UserID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> AcceptFriendRequest(AcceptFriendRequestRequest request)
        {
            AcceptFriendRequestResponse response = new AcceptFriendRequestResponse();
            try
            {
                response = await _accountSL.AcceptFriendRequest(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> CancleFriendRequest([FromQuery] int FriendRequestID)
        {
            CancleFriendRequestResponse response = new CancleFriendRequestResponse();
            try
            {
                response = await _accountSL.CancleFriendRequest(FriendRequestID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        //
        [HttpPost]
        public async Task<IActionResult> SearchPeople(SearchPeopleRequest request)
        {
            SearchPeopleResponse response = new SearchPeopleResponse();
            try
            {
                response = await _accountSL.SearchPeople(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPost([FromForm] InsertPostRequest request)
        {
            InsertPostResponse response = new InsertPostResponse();
            try
            {
                response = await _accountSL.InsertPost(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            GetPostResponse response = new GetPostResponse();
            try
            {
                response = await _accountSL.GetPost();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost([FromQuery] int PostID)
        {
            DeletePostResponse response = new DeletePostResponse();
            try
            {
                response = await _accountSL.DeletePost(PostID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }

            return Ok(response);
        }

    }
}
