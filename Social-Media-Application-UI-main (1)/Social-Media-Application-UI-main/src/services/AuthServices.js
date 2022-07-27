import AxiosServices from "./AxiosServices";
import AuthConfigurations from "../configuration/AuthConfigurations";

const axiosServices = new AxiosServices();

export default class AuthServices {
  SignUp(data) {
    return axiosServices.post(AuthConfigurations.SignUp, data, false);
  }

  SignIn(data) {
    return axiosServices.post(AuthConfigurations.SignIn, data, false);
  }

}
