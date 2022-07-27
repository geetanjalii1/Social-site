import AxiosServices from "./AxiosServices";
import HomePageConfiguration from "../configuration/HomePageConfiguration";

const axiosServices = new AxiosServices();

export default class HomePageService {
  GetAllFriendList(Data) {
    return axiosServices.Get(
      HomePageConfiguration.GetAllFriendList + Data,
      false
    );
  }
  GetAllUserList(Data) {
    return axiosServices.Get(
      HomePageConfiguration.GetAllUserList + Data,
      false
    );
  }
  SendFriendRequest(Data) {
    return axiosServices.post(
      HomePageConfiguration.SendFriendRequest,
      Data,
      false
    );
  }
  GetAllFriendRequestList(Data) {
    return axiosServices.Get(
      HomePageConfiguration.GetAllFriendRequestList + Data,
      false
    );
  }
  RejectFriendRequest(Data) {
    return axiosServices.Get(
      HomePageConfiguration.RejectFriendRequest + Data,
      false
    );
  }
  GetSendFriendRequestList(Data) {
    return axiosServices.Get(
      HomePageConfiguration.GetSendFriendRequestList + Data,
      false
    );
  }
  AcceptFriendRequest(Data) {
    return axiosServices.Patch(
      HomePageConfiguration.AcceptFriendRequest,
      Data,
      false
    );
  }

  CancleFriendRequest(Data) {
    return axiosServices.Delete(
      HomePageConfiguration.CancleFriendRequest + Data,
      false
    );
  }

  SearchPeople(Data) {
    return axiosServices.post(HomePageConfiguration.SearchPeople, Data, false);
  }

  InsertPost(Data) {
    return axiosServices.post(HomePageConfiguration.InsertPost, Data, false);
  }

  GetPost() {
    return axiosServices.Get(HomePageConfiguration.GetPost, false);
  }

  DeletePost(Data) {
    return axiosServices.Delete(HomePageConfiguration.DeletePost + Data, false);
  }
}
