import React, { Component } from "react";
import "./HomePage.scss";
import GetPost from "./Sub-Component/GetPost";
import HomePageService from "../services/HomePageService";
import FriendList from "./Sub-Component/FriendList";
import PeopleList from "./Sub-Component/PeopleList";
import EmojiEmotionsIcon from "@material-ui/icons/EmojiEmotions";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import SendIcon from "@material-ui/icons/Send";
import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import Backdrop from "@material-ui/core/Backdrop";
import CircularProgress from "@material-ui/core/CircularProgress";
import SearchIcon from "@material-ui/icons/Search";
import InputBase from "@material-ui/core/InputBase";
import Modal from "@material-ui/core/Modal";
import Fade from "@material-ui/core/Fade";
import AddIcon from "@material-ui/icons/Add";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import PhotoCamera from "@material-ui/icons/PhotoCamera";
import Demo from "./../Assert/AddPost.png";
const homeServices = new HomePageService();

export default class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      // UserID: this.props.location.state.UserID,
      UserID: localStorage.getItem("UserID"),
      FullName: localStorage.getItem("FullName"),

      FriendList: [],
      PeopleList: [],
      SendRequestList: [],
      FriendRequest: [],
      PostList: [],

      PostType: "Image",
      Image1: Demo,
      Image: new FormData(),
      ImageFlag: false,
      PostText: "",
      Caption: "",
      PostTextFlag: false,

      OpenLoader: false,
      OpenSnackBar: false,
      Message: "",

      SearchText: "",

      IsSearch: false,
      Open: false,
    };
  }

  componentDidMount() {
    console.log("Component Did Mount Calling....");
    this.GetPost();
    this.GetAllFriendRequestList(this.state.UserID);
    this.GetAllFriendList(this.state.UserID);
    this.GetAllUserList(this.state.UserID);
    this.GetSendFriendRequestList(this.state.UserID);
  }

  GetAllUserList = (UserID) => {
    console.log("GetAllUserList UserID : ", UserID);
    homeServices
      .GetAllUserList(UserID)
      .then((data) => {
        console.log("GetAllUserList Data : ", data);
        if (data.data.isSuccess) {
          this.setState({ PeopleList: data.data.data });
        } else {
          this.setState({});
        }
      })
      .catch((error) => {
        console.log("GetAllUserList Error : ", error);
      });
  };

  GetSendFriendRequestList = (UserID) => {
    if (this.state.IsSearch) {
      return;
    }

    console.log("GetSendFriendRequestList UserID : ", UserID);
    homeServices
      .GetSendFriendRequestList(UserID)
      .then((data) => {
        console.log("GetSendFriendRequestList Data : ", data);
        if (data.data.isSuccess) {
          this.setState({ SendRequestList: data.data.data });
        } else {
          this.setState({});
        }
      })
      .catch((error) => {
        console.log("GetSendFriendRequestList Error : ", error);
      });
  };

  GetAllFriendRequestList = (UserID) => {
    console.log("GetAllFriendRequestList UserID : ", UserID);
    homeServices
      .GetAllFriendRequestList(UserID)
      .then((data) => {
        console.log("GetAllFriendRequestList Data : ", data);
        if (data.data.isSuccess) {
          this.setState({ FriendRequest: data.data.data });
        } else {
          this.setState({});
        }
      })
      .catch((error) => {
        console.log("GetAllFriendRequestList Error : ", error);
      });
  };

  GetAllFriendList = (UserID) => {
    console.log("GetAllFriendList UserID : ", UserID);
    homeServices
      .GetAllFriendList(UserID)
      .then((data) => {
        console.log("GetAllFriendList Data : ", data);
        if (data.data.isSuccess) {
          this.setState({ FriendList: data.data.data });
        } else {
          this.setState({});
        }
      })
      .catch((error) => {
        console.log("GetAllFriendList Error : ", error);
      });
  };

  GetPost = () => {
    console.log("GetPost UserID : ");
    homeServices
      .GetPost()
      .then((data) => {
        console.log("GetPost Data : ", data);
        if (data.data.isSuccess) {
          this.setState({ PostList: data.data.data });
        } else {
          this.setState({});
        }
      })
      .catch((error) => {
        console.log("GetPost Error : ", error);
      });
  };

  handleChange = (e) => {
    const { name, value } = e.target;

    this.setState({ [name]: value }, console.log("Search Text : ", value));
    if (value === "") {
      this.GetAllUserList(Number(this.state.UserID));
      this.GetSendFriendRequestList(Number(this.state.UserID));
      this.setState({ IsSearch: false });
    }
  };

  handleRadioChange = (event) => {
    this.setState({ PostType: event.target.value });
  };

  handleCapture = (event) => {
    console.log("handleCapture Calling ...", event);
    const reader = new FileReader();
    reader.onload = () => {
      this.setState({ Image1: reader.result });
    };
    reader.readAsDataURL(event.target.files[0]);
    this.setState({ Image: event.target.files[0] });
  };

  UploadPost = () => {
    this.setState({ Open: true });
  };

  handleClose = () => {
    this.setState({ Open: false, OpenSnackBar: false });
  };

  SearchPeople = () => {
    console.log("SearchPeople UserID : ");
    this.setState({ IsSearch: true, SendRequestList: null });
    let data = {
      userID: Number(this.state.UserID),
      searchKey: this.state.SearchText,
    };
    homeServices
      .SearchPeople(data)
      .then((data) => {
        console.log("SearchPeople Data : ", data);
        this.setState({ IsSearch: false });
        if (data.data.isSuccess) {
          this.setState({ PeopleList: data.data.data });
        } else {
          this.setState({ PeopleList: null });
        }
      })
      .catch((error) => {
        console.log("SearchPeople Error : ", error);
        this.setState({ IsSearch: false });
      });
  };

  CheckValidity = () => {
    //
    this.setState({ PostTextFlag: false, ImageFlag: false });
    if (this.state.PostType === "Text" && this.state.PostText === "") {
      this.setState({ PostTextFlag: true });
    }
    //
    if (this.state.PostType === "Image" && this.state.Image === Demo) {
      this.setState({ ImageFlag: true });
    }
  };

  handleUploadPost = async () => {
    console.log("handleUploadPost Calling.....");

    await this.CheckValidity();

    if (this.state.PostType === "Text" && this.state.PostText === "") {
      this.setState({ OpenSnackBar: true, Message: "Please Enter Text" });
      return;
    }

    if (this.state.PostType === "Image" && this.state.Image1 === Demo) {
      this.setState({ OpenSnackBar: true, Message: "Please Enter Image" });
      return;
    }

    this.setState({ OpenLoader: true });
    const data1 = new FormData();
    data1.append("file1", this.state.Image);
    data1.append("userID", this.state.UserID);
    data1.append("fileType", this.state.PostType);
    data1.append("file2", this.state.PostText);
    data1.append("caption", this.state.Caption);
    homeServices
      .InsertPost(data1)
      .then((data) => {
        console.log(" handleUploadPost Data : ", data);
        this.setState({
          OpenLoader: false,
          OpenSnackBar: true,
          Open: false,
          Message: data.data.message,
        });
        this.GetPost();
      })
      .catch((error) => {
        console.log("handleUploadPost Error : ", error);
        this.setState({
          OpenLoader: false,
          OpenSnackBar: true,
          Open: false,
          Message: "Something Went Wrong",
        });
        this.GetPost();
      });
  };

  render() {
    console.log("State : ", this.state);
    return (
      <div className="HomePage-Container">
        <div className="HomePage-SubContainer">
          <div className="Header">
            <div className="Nav1">Friends</div>
            <div className="main-Body">
              <div>
                <div>Welcome {this.state.FullName}</div>
                <div>
                  <Button
                    onClick={() => {
                      this.UploadPost();
                    }}
                  >
                    <AddIcon />
                  </Button>
                </div>
              </div>
              <div className="SocialMedia">
                <EmojiEmotionsIcon />
                &nbsp;Social Media App
              </div>
            </div>
            <div className="Nav2">
              <div className="search" style={{ flexGrow: 0.5 }}>
                <div
                  className="searchIcon"
                  // style={{ cursor: "pointer" }}
                >
                  <IconButton
                    style={{ cursor: "pointer" }}
                    onClick={() => {
                      this.SearchPeople();
                    }}
                  >
                    <SearchIcon />
                  </IconButton>
                </div>
                <InputBase
                  placeholder="Search People"
                  name="SearchText"
                  classes={{
                    root: "inputRoot",
                    input: "inputInput",
                  }}
                  inputProps={{ "aria-label": "search" }}
                  value={this.state.SearchText}
                  onChange={this.handleChange}
                />
              </div>
            </div>
          </div>
          <div className="Body">
            <div className="Nav1">
              <FriendList
                FriendList={this.state.FriendList}
                FriendRequest={this.state.FriendRequest}
                GetSendFriendRequestList={this.GetSendFriendRequestList}
                GetAllFriendRequestList={this.GetAllFriendRequestList}
                GetAllFriendList={this.GetAllFriendList}
                GetAllUserList={this.GetAllUserList}
              />
            </div>
            <div className="main-Body">
              <GetPost PostList={this.state.PostList} />
            </div>
            <div className="Nav2">
              <PeopleList
                PeopleList={this.state.PeopleList}
                SendRequestList={this.state.SendRequestList}
                GetAllUserList={this.GetAllUserList}
                GetSendFriendRequestList={this.GetSendFriendRequestList}
              />
            </div>
          </div>
          <div className="Footer">
            <div className="Nav1"></div>
            <div className="main-Body">
              <TextField
                id="outlined-basic"
                size="small"
                variant="outlined"
                style={{ width: 325 }}
              />
              <Button variant="contained" color="secondary">
                <SendIcon />
                &nbsp;Send
              </Button>
            </div>
            <div className="Nav2"></div>
          </div>
        </div>

              
        <Modal
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
          open={this.state.Open}
          onClose={this.handleClose}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500,
          }}
        >
          <Fade in={this.state.Open}>
            <div
              style={{
                backgroundColor: "white",
                boxShadow: "5",
                padding: "2px 4px 3px",
                width: "500px",
                height: "600px",
                display: "flex",
                alignItems: "center",
                justifyContent: "flex-start",
                flexDirection: "column",
              }}
            >
              <div style={{ display: "flex", flexDirection: "row" }}>
                <div>
                  <div className="Input-Field1">
                    <div className="Text">
                      <AccountCircleIcon fontSize="large" />
                    </div>
                    <div
                      style={{
                        color: "blue",
                        fontFamily: "Roboto",
                        fontWeight: "500",
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                      }}
                    >
                      {this.state.FullName}
                    </div>
                  </div>
                  <div className="Input-Field1">
                    <div className="Text">Post Type</div>
                    <div
                      style={{
                        color: "blue",
                        fontFamily: "Roboto",
                        fontWeight: "500",
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                      }}
                    >
                      <RadioGroup
                        name="PostType"
                        value={this.state.PostType}
                        onChange={this.handleRadioChange}
                        style={{ display: "flex", flexDirection: "row" }}
                      >
                        <FormControlLabel
                          value="Image"
                          control={<Radio />}
                          label="Image"
                        />
                        <FormControlLabel
                          value="Text"
                          control={<Radio />}
                          label="Text"
                        />
                      </RadioGroup>
                    </div>
                  </div>
                  {this.state.PostType == "Image" ? (
                    <div
                      className="UploadImage"
                      style={
                        !this.state.ImageFlag
                          ? { border: "0.5px solid lightgray" }
                          : { border: "0.5px solid red" }
                      }
                    >
                      <img
                        src={this.state.Image1}
                        alt="Post Image"
                        style={{ height: 200, width: 200 }}
                      />
                      <input
                        accept="image/*"
                        style={{ display: "none" }}
                        id="icon-button-file"
                        type="file"
                        onChange={this.handleCapture}
                      />
                      <label htmlFor="icon-button-file">
                        <IconButton
                          color="primary"
                          aria-label="upload picture"
                          component="span"
                        >
                          <PhotoCamera />
                        </IconButton>
                      </label>
                    </div>
                  ) : (
                    <div className="UploadText">
                      <TextField
                        error={this.state.PostTextFlag}
                        label="Whats New On Your Mind ?"
                        variant="outlined"
                        name="PostText"
                        multiline
                        rows={10}
                        style={{ width: "inherit", height: "inherit" }}
                        value={this.state.PostText}
                        onChange={this.handleChange}
                      />
                    </div>
                  )}
                  <div className="Caption">
                    {this.state.PostType === "Image" ? (
                      <TextField
                        label="Caption"
                        variant="outlined"
                        size="small"
                        name="Caption"
                        style={{ width: 400 }}
                        value={this.state.Caption}
                        onChange={this.handleChange}
                      />
                    ) : null}
                  </div>
                  <div
                    style={{
                      width: 400,
                      margin: "10px auto",
                      display: "flex",
                      justifyContent: "flex-end",
                    }}
                  >
                    <Button
                      variant="contained"
                      color="secondary"
                      onClick={() => {
                        this.handleUploadPost();
                      }}
                    >
                      Upload Post
                    </Button>
                  </div>
                </div>
              </div>
            </div>
          </Fade>
        </Modal>

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
          open={this.state.OpenSnackBar}
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
