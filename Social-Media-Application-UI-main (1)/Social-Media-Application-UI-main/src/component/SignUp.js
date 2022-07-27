import React, { Component } from "react";
import "./SignUp.css";

import AuthServices from "../services/AuthServices.js";
import TextField from "@material-ui/core/TextField";
import Backdrop from "@material-ui/core/Backdrop";
import CircularProgress from "@material-ui/core/CircularProgress";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";

import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";

const authServices = new AuthServices();

const PasswordRegex = RegExp(
  /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/i
);

export default class SignUp extends Component {
  constructor() {
    super();
    this.state = {
      FullName: "",
      UserName: "",
      Password: "",
      ConfirmPassword: "",
      MasterPassword: "",

      FullNameFlag: false,
      UserNameFlag: false,
      PasswordFlag: false,
      ConfirmPasswordFlag: false,
      MasterPasswordFlag: false,

      open: false,
      OpenLoader: false,
      Message: "",
    };
  }

  handleClose = (e, reason) => {
    if (reason === "clickaway") {
      return;
    }
    this.setState({ open: false });
  };

  CheckValidity() {
    console.log("Check Validity Calling");
    //Reset Flag
    this.setState({
      FullNameFlag: false,
      UserNameFlag: false,
      PasswordFlag: false,
      ConfirmPasswordFlag: false,
      MasterPasswordFlag: false,
    });

    if (this.state.FullName === "") {
      this.setState({ FullNameFlag: true });
    }

    if (this.state.UserName === "") {
      this.setState({ UserNameFlag: true });
    }
    if (this.state.Password === "") {
      this.setState({ PasswordFlag: true });
    }
    if (this.state.ConfirmPassword === "") {
      this.setState({ ConfirmPasswordFlag: true });
    }
  }

  handleSubmit = (e) => {
    this.setState({ OpenLoader: true });
    if (!this.state.PasswordFlag) {
      this.CheckValidity();
      if (
        this.state.FullName !== "" &&
        this.state.UserName !== "" &&
        this.state.Password !== "" &&
        this.state.ConfirmPassword !== ""
      ) {
        const data = {
          fullName: this.state.FullName,
          userName: this.state.UserName,
          password: this.state.Password,
          configPassword: this.state.ConfirmPassword,
        };

        authServices
          .SignUp(data)
          .then((data) => {
            console.log("data : ", data);
            if (data.data.isSuccess) {
              this.setState({ OpenLoader: false });
              this.props.history.push("/SignIn");
            } else {
              console.log("Sign Up Failed");
              this.setState({
                open: true,
                Message: data.data.message,
                OpenLoader: false,
              });
            }
          })
          .catch((error) => {
            console.log("error : ", error);
            this.setState({
              open: true,
              Message: "Something Went Wrong",
              OpenLoader: false,
            });
          });
      } else {
        console.log("Not Acceptable");
        this.setState({
          open: true,
          Message: "Please Fill Required Field",
          OpenLoader: false,
        });
      }
    } else {
      this.setState({
        open: true,
        Message: "Please Fill Valid Password",
        OpenLoader: false,
      });
    }
  };

  handleChangePassword = (e) => {
    const { name, value } = e.target;
    console.log("Regex Match : ", PasswordRegex.test(value));
    if (!PasswordRegex.test(value)) {
      this.setState({ PasswordFlag: true });
    } else {
      this.setState({ PasswordFlag: false });
    }
    this.setState(
      { [name]: value },
      console.log(
        "Name : ",
        name,
        "Value : ",
        value,
        " PasswordFlag : ",
        this.state.PasswordFlag
      )
    );
  };

  handleChange = (e) => {
    const { name, value } = e.target;

    this.setState(
      { [name]: value },
      console.log("Name : ", name, "Value : ", value)
    );
  };

  handleSignIn = (e) => {
    this.props.history.push("/SignIn");
  };

  render() {
    console.log("state : ", this.state);
    return (
      <div className="SignUp-Container">
        <div className="SignUp-SubContainer">
          <div className="Title">Social Media</div>
          <div className="Header_Container">Sign Up</div>
          <div className="Body">
            <form className="form">
              <TextField
                className="TextField"
                name="FullName"
                label="FullName"
                variant="outlined"
                size="small"
                style={{ margin: 12 }}
                error={this.state.FullNameFlag}
                value={this.state.FullName}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="UserName"
                label="UserName"
                variant="outlined"
                size="small"
                style={{ margin: 12 }}
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
                style={{ margin: 12 }}
                error={this.state.PasswordFlag}
                value={this.state.Password}
                onChange={this.handleChangePassword}
              />
              {this.state.PasswordFlag ? (
                <div className="PassError">
                  Password Must Contain Upper Letter, Lower Letter, Symbol &
                  Number.
                </div>
              ) : (
                <></>
              )}
              <TextField
                className="TextField"
                type="password"
                name="ConfirmPassword"
                label="Confirm Password"
                variant="outlined"
                size="small"
                style={{ margin: 12 }}
                error={this.state.ConfirmPasswordFlag}
                value={this.state.ConfirmPassword}
                onChange={this.handleChange}
              />
            </form>
          </div>
          <div className="Buttons">
            <Button className="Btn" color="primary" onClick={this.handleSignIn}>
              Sign In
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              onClick={this.handleSubmit}
            >
              Sign Up
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
