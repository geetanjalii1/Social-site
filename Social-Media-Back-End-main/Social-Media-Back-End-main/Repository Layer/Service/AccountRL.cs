using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Service
{
    public class AccountRL : IAccountRL
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _SqlConnection;
        private readonly ILogger<AccountRL> _logger;
        public AccountRL(IConfiguration configuration, ILogger<AccountRL> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _SqlConnection = new SqlConnection(_configuration["ConnectionStrings:DBSettingConnection"]);
        }

        public async Task<AcceptFriendRequestResponse> AcceptFriendRequest(AcceptFriendRequestRequest request)
        {
            AcceptFriendRequestResponse response = new AcceptFriendRequestResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                    UPDATE FriendRequestTable
                                    SET IsAccept=1
                                    WHERE FriendRequestID=@FriendRequestID
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@FriendRequestID", request.FriendRequestID); //Current User ID
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In AcceptFriendRequest Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<CancleFriendRequestResponse> CancleFriendRequest(int FriendRequestID)
        {
            CancleFriendRequestResponse response = new CancleFriendRequestResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                DELETE FriendRequestTable
                                WHERE FriendRequestID=@FriendRequestID
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@FriendRequestID", FriendRequestID); //Current User ID
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In SendFriendRequest Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<DeletePostResponse> DeletePost(int PostID)
        {
            DeletePostResponse response = new DeletePostResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                UPDATE PostDetails
                                SET IsDelete=1
                                WHERE PostID=@PostID
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@PostID", PostID); //Current User ID
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In DeletePost Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetAllFriendListResponse> GetAllFriendList(int UserID)
        {
            GetAllFriendListResponse response = new GetAllFriendListResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                SELECT Distinct F.FriendRequestID, 
                                       (SELECT FullName FROM UserDetail WHERE UserID=F.FromUserID) AS FromFullName,
									   (SELECT FullName FROM UserDetail WHERE UserID=F.ToUserID) AS ToFullName
                                FROM FriendRequestTable AS F
                                WHERE (F.ToUserID=@UserID OR F.FromUserID=@UserID ) AND F.IsAccept = 1; 
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<GetAllFriendList>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        GetAllFriendList data = new GetAllFriendList();
                                        data.FriendRequestID = dbDataReader["FriendRequestID"] != DBNull.Value ? (Int32)dbDataReader["FriendRequestID"] : -1;
                                        data.FromFullName = dbDataReader["FromFullName"] != DBNull.Value ? (string)dbDataReader["FromFullName"] : string.Empty;
                                        data.ToFullName = dbDataReader["ToFullName"] != DBNull.Value ? (string)dbDataReader["ToFullName"] : string.Empty;
                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await _SqlConnection.CloseAsync();
                        await _SqlConnection.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetAllFriendRequestListResponse> GetAllFriendRequestList(int UserID)
        {
            GetAllFriendRequestListResponse response = new GetAllFriendRequestListResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                    SELECT F.FriendRequestID, (SELECT FullName FROM UserDetail Where UserID=F.FromUserID) AS FullName
                                    From FriendRequestTable F
                                    WHERE F.ToUserID=@UserID AND F.IsAccept=0 AND F.IsReject=0
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<GetAllFriendRequestList>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        GetAllFriendRequestList data = new GetAllFriendRequestList();
                                        data.FriendRequestID = dbDataReader["FriendRequestID"] != DBNull.Value ? (Int32)dbDataReader["FriendRequestID"] : -1;
                                        data.FullName = dbDataReader["FullName"] != DBNull.Value ? (string)dbDataReader["FullName"] : string.Empty;
                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {
                                await dbDataReader.DisposeAsync();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetAllUserListResponse> GetAllUserList(int UserID)
        {
            GetAllUserListResponse response = new GetAllUserListResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                // 1] Not Self Account
                // 2] Send Request Account Not Show
                // 3] 
                // 4] 

                string SqlQuery = @"
                                  select U.UserID, U.FullName 
                                  from UserDetail U 
                                  Where U.UserID <> @UserID and 
		                                U.UserID not in (	select D.FromUserID 
						                                from FriendRequestTable D 
						                                where D.ToUserID=@UserID and 
							                                  D.IsAccept=0 and D.IsReject=0
					                                ) and
                                        U.UserID not in (	select D.ToUserID 
						                                from FriendRequestTable D 
						                                where D.FromUserID=@UserID and 
							                                  D.IsAccept=1
					                                ) and
                                        U.UserID not in (	select D.FromUserID 
					                                    from FriendRequestTable D 
					                                    where D.ToUserID=@UserID and 
							                                    D.IsAccept=1
				                                    ) and
                                        U.UserID not in (	select D.ToUserID 
						                                from FriendRequestTable D 
						                                where D.FromUserID=@UserID and 
							                                  D.IsAccept=0 and D.IsReject=0
					                                );
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<GetAllUserList>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        GetAllUserList data = new GetAllUserList();
                                        data.ToUserID = dbDataReader["UserID"] != DBNull.Value ? (Int32)dbDataReader["UserID"] : -1;
                                        data.ToUserName = dbDataReader["FullName"] != DBNull.Value ? (string)dbDataReader["FullName"] : string.Empty;
                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {
                                await dbDataReader.DisposeAsync();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetPostResponse> GetPost()
        {
            GetPostResponse response = new GetPostResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                  SELECT *, (SELECT FullName FROM UserDetail WHERE UserID=P.UserID) as FullName 
                                  FROM PostDetails P
                                  WHERE IsDelete=0 
                                  ORDER BY InsertionDate DESC;
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<GetPost>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        //PostID InsertionDate UserID PostType Value PublicID LikeCount LikeData
                                        GetPost data = new GetPost();
                                        data.PostID = dbDataReader["PostID"] != DBNull.Value ? (Int32)dbDataReader["PostID"] : -1;
                                        data.InserttionDate = dbDataReader["InsertionDate"] != DBNull.Value ? Convert.ToDateTime(dbDataReader["InsertionDate"]).ToString("dddd, dd-MM-yyyy hh:mm tt") : string.Empty;
                                        data.UserID = dbDataReader["UserID"] != DBNull.Value ? (Int32)dbDataReader["UserID"] : -1;
                                        data.PostType = dbDataReader["PostType"] != DBNull.Value ? (string)dbDataReader["PostType"] : string.Empty;
                                        data.Value = dbDataReader["Value"] != DBNull.Value ? (string)dbDataReader["Value"] : string.Empty;
                                        data.PublicID = dbDataReader["PublicID"] != DBNull.Value ? (string)dbDataReader["PublicID"] : string.Empty;
                                        data.Like = dbDataReader["LikeCount"] != DBNull.Value ? (Int32)dbDataReader["LikeCount"] : 0;
                                        data.LikeDate = dbDataReader["LikeData"] != DBNull.Value ? (string)dbDataReader["LikeData"] : string.Empty;
                                        data.FullName = dbDataReader["FullName"] != DBNull.Value ? Convert.ToString(dbDataReader["FullName"]) : string.Empty;
                                        if (data.PostType.ToLower() == "image")
                                        {
                                            data.data = JsonConvert.DeserializeObject<ImageTypeRequest>(data.Value);
                                        }

                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {
                                await dbDataReader.DisposeAsync();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }finally 
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<GetSendFriendRequestListResponse> GetSendFriendRequestList(int UserID)
        {
            GetSendFriendRequestListResponse response = new GetSendFriendRequestListResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                    select D.FriendRequestID,  
                                           (SELECT FullName From UserDetail Where UserID=D.ToUserID) as FullName
                                    from FriendRequestTable D 
                                    where D.FromUserID=@UserID and 
		                                    D.IsAccept=0 and D.IsReject=0
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<GetSendFriendRequestList>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        GetSendFriendRequestList data = new GetSendFriendRequestList();
                                        data.FriendRequestID = dbDataReader["FriendRequestID"] != DBNull.Value ? (Int32)dbDataReader["FriendRequestID"] : -1;
                                        data.FullName = dbDataReader["FullName"] != DBNull.Value ? (string)dbDataReader["FullName"] : string.Empty;
                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {
                                await dbDataReader.DisposeAsync();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<InsertPostResponse> InsertPost(InsertPostRequest request)
        {
            InsertPostResponse response = new InsertPostResponse();
            ImageTypeRequest ImageRequest = new ImageTypeRequest();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string Url = string.Empty, PublicID = string.Empty;
                if (request.FileType.ToString().ToLower() == "image")
                {

                    Account account = new Account(
                                    _configuration["CloudinarySettings:CloudName"],
                                    _configuration["CloudinarySettings:ApiKey"],
                                    _configuration["CloudinarySettings:ApiSecret"]);

                    var path = request.File1.OpenReadStream();

                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(request.File1.FileName, path),
                        //Folder=""
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    ImageRequest.ImageUrl = uploadResult.Url.ToString();
                    PublicID = uploadResult.PublicId.ToString();

                    ImageRequest.Caption = request.Caption;
                    

                }

                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }
                string SqlQuery = string.Empty;
                if (request.FileType.ToString().ToLower() == "image")
                {
                    SqlQuery = @"
                                    INSERT INTO PostDetails
                                    (UserID, PostType, Value, PublicID) 
                                    VALUES
                                    (@UserID, @PostType, @Value, @PublicID)
                                ";
                }
                else
                {
                    SqlQuery = @"
                                    INSERT INTO PostDetails
                                    (UserID, PostType, Value) 
                                    VALUES
                                    (@UserID, @PostType, @Value)
                                ";
                }

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", request.UserID); //Current User ID
                        sqlCommand.Parameters.AddWithValue("@PostType", request.FileType.ToString().ToLower()); //Send Request User ID
                        sqlCommand.Parameters.AddWithValue("@Value", request.FileType.ToString().ToLower() == "text" ? request.File2 : JsonConvert.SerializeObject(ImageRequest));
                        if (request.FileType.ToString().ToLower() == "image")
                        {
                            sqlCommand.Parameters.AddWithValue("@PublicID", PublicID);
                        }

                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In SendFriendRequest Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<RejectFriendRequestResponse> RejectFriendRequest(int request)
        {
            RejectFriendRequestResponse response = new RejectFriendRequestResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                UPDATE FriendRequestTable
                                SET IsReject=1 
                                WHERE FriendRequestID=@FriendRequestID
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@FriendRequestID", request); //Current User ID
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In SendFriendRequest Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<SearchPeopleResponse> SearchPeople(SearchPeopleRequest request)
        {
            SearchPeopleResponse response = new SearchPeopleResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                    SELECT U.UserID, U.FullName 
                                    From UserDetail U 
                                    WHERE U.FullName Like '%" + request.SearchKey + @"%' AND 
	                                      U.UserID <> @UserID AND (U.UserID <> ( SELECT F.ToUserID FROM FriendRequestTable F WHERE F.FromUserID=@UserID AND F.IsAccept=1)
						                                      OR U.UserID <> ( SELECT F.FromUserID FROM FriendRequestTable F WHERE F.ToUserID=@UserID AND F.IsAccept=1))
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserID", request.UserID);
                        sqlCommand.Parameters.AddWithValue("@SearchText", request.SearchKey);

                        using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            try
                            {
                                if (dbDataReader.HasRows)
                                {
                                    response.data = new List<SearchPeople>();
                                    while (await dbDataReader.ReadAsync())
                                    {
                                        SearchPeople data = new SearchPeople();
                                        data.toUserID = dbDataReader["UserID"] != DBNull.Value ? (Int32)dbDataReader["UserID"] : -1;
                                        data.toUserName = dbDataReader["FullName"] != DBNull.Value ? (string)dbDataReader["FullName"] : string.Empty;
                                        response.data.Add(data);
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "Exception Occurs : Message : " + ex.Message;
                            }
                            finally
                            {
                                await dbDataReader.DisposeAsync();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<SendFriendRequestResponse> SendFriendRequest(SendFriendRequestRequest request)
        {
            SendFriendRequestResponse response = new SendFriendRequestResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_SqlConnection != null && _SqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _SqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                INSERT INTO FriendRequestTable(FromUserID, ToUserID) VALUES (@FromUserID, @ToUserID)
                                ";

                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _SqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@FromUserID", request.FromUserID); //Current User ID
                        sqlCommand.Parameters.AddWithValue("@ToUserID", request.ToUserID); //Send Request User ID
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "Something Went Wrong In SendFriendRequest Query";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Exception Occurs : Message : " + ex.Message;
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : Message : " + ex.Message;
            }
            finally
            {
                await _SqlConnection.CloseAsync();
                await _SqlConnection.DisposeAsync();
            }

            return response;
        }

    }
}
