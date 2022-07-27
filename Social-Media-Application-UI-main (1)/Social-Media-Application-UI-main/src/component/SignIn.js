import React, { Component } from "react";
import AuthServices from "../services/AuthServices";
import "./SignIn.css";
import TextField from "@material-ui/core/TextField";
import Radio from "@material-ui/core/Radio";
import Backdrop from "@material-ui/core/Backdrop";
import CircularProgress from "@material-ui/core/CircularProgress";
import Button from "@material-ui/core/Button";
import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";

const authServices = new AuthServices();

export default class SignIn extends Component {
  constructor() {
    super();
    this.state = {
      OpenLoader: false,
      UserName: "",
      UserNameFlag: false,
      Password: "",
      PasswordFlag: false,
      open: false,
      Message: "",
    };
  }

  handleClose = (e, reason) => {
    if (reason === "clickaway") {
      return;
    }
    this.setState({ open: false });
  };

  handleRadioChange = (e) => {
    this.setState({ Radiovalue: e.target.value });
  };

  handleChange = (e) => {
    const { name, value } = e.target;
    this.setState(
      { [name]: value },
      console.log("Name : ", name, "Value : ", value)
    );
  };

  handleSignUp = (e) => {
    this.props.history.push("/");
  };

  CheckValidation() {
    console.log("CheckValidation Calling...");

    this.setState({ UserNameFlag: false, PasswordFlag: false });

    if (this.state.UserName === "") {
      this.setState({ UserNameFlag: true });
    }
    if (this.state.Password === "") {
      this.setState({ PasswordFlag: true });
    }
  }

  handleSubmit = (e) => {
    this.CheckValidation();
    if (this.state.UserName !== "" && this.state.Password !== "") {
      this.setState({ OpenLoader: true });
      console.log("Acceptable");
      let data = {
        userName: this.state.UserName,
        password: this.state.Password,
      };
      authServices
        .SignIn(data)
        .then((data) => {
          console.log("Sign In Data : ", data);
          this.setState({ OpenLoader: false });
          if (data.data.isSuccess) {
            localStorage.setItem("Token", data.data.data.token);
            localStorage.setItem("UserID", data.data.data.userId);
            localStorage.setItem("FullName", data.data.data.fullName);
            this.props.history.push({
              pathname: "/HomePage",
              state: { UserID: data.data.data.userId },
            });
          } else {
            console.log("Something Went Wrong");
            this.setState({ open: true, Message: data.data.message });
          }
        })
        .catch((error) => {
          console.log("Sign In Error : ", error);
          this.setState({
            open: true,
            Message: "Something Went Wrong",
            OpenLoader: false,
          });
        });
    } else {
      console.log("Not Acceptable");
      this.setState({ open: true, Message: "Please Field Mandetory Field" });
    }
  };

  render() {
    console.log("State : ", this.state);
    return (
      <div className="SignIn-Container">
        <div className="SignUp-SubContainer">
          <div className="Title">Social Media</div>
          <div className="Header_Container">Log In</div>
          <div className="Body">
            <form className="form">
              <TextField
                className="TextField"
                name="UserName"
                label="UserName"
                variant="outlined"
                size="small"
                style={{ margin: 20 }}
                error={this.state.UserNameFlag}
                value={this.state.UserName}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                type="password"
                name="Password"
                label="Password"
                variant="outlined"
                size="small"
                style={{ margin: 20 }}
                error={this.state.PasswordFlag}
                value={this.state.Password}
                onChange={this.handleChange}
              />
            </form>
          </div>
          <div className="Buttons" style={{ alignItems: "flex-start" }}>
            <Button className="Btn" color="primary" onClick={this.handleSignUp}>
              Create New Account
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              onClick={this.handleSubmit}
            >
              Sign In
            </Button>
          </div>
        </div>
        <Backdrop
          style={{ zIndex: "1", color: "#fff" }}
          open={this.state.OpenLoader}
        >
          <CircularProgress color="inherit" />
        </Backdrop>
        <Snackbar
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
          open={this.state.open}
          autoHideDuration={6000}
          onClose={this.handleClose}
          message={this.state.Message}
          action={
            <React.Fragment>
              <Button color="secondary" size="small" onClick={this.handleClose}>
                UNDO
              </Button>
              <IconButton
                size="small"
                aria-label="close"
                color="inherit"
                onClick={this.handleClose}
              >
                <CloseIcon fontSize="small" />
              </IconButton>
            </React.Fragment>
          }
        />
      </div>
    );
  }
}
