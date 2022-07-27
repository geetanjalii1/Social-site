import React from "react";
import "./GetPost.scss";
import AccountCircleIcon from "@material-ui/icons/AccountCircle";
import ThumbUpAltIcon from "@material-ui/icons/ThumbUpAlt";

export default function GetPost(props) {
  return (
    <div className="GetPost-Container">
      <div className="GetPost-SubContainer">
        {Array.isArray(props.PostList) && props.PostList.length > 0 ? (
          props.PostList.map(function (data, index) {
            return (
              <div className="GetPost-DataFlex" key={index}>
                <div className="GetPost-Header">
                  <AccountCircleIcon fontSize="large" />
                  &nbsp;
                  <div className="Account-Text">{data.fullName}</div>
                </div>
                <div className="GetPost-Body">
                  {data.postType === "image" ? (
                    <img
                      src={data.data.imageUrl}
                      style={{ height: 250, width: 250 }}
                    />
                  ) : (
                    <div className="GetPost-Body-Text">{data.value}</div>
                  )}
                </div>
                <div className="GetPost-Footer">
                  <div className="GetPost-Footer-Operation">
                    <ThumbUpAltIcon /> &nbsp;{" "}
                    <div className="GetPost-Footer-Operation-Text">
                      15 Likes
                    </div>
                  </div>
                  <div className="GetPost-Footer-Caption">
                    <div className="GetPost-Footer-Caption-UserName">
                      {data.fullName}
                    </div>
                    <div className="GetPost-Footer-Caption-Text">
                      {data.postType === "image" ? (
                        <>{data.data.caption}</>
                      ) : null}
                    </div>
                  </div>
                </div>
              </div>
            );
          })
        ) : (
          <>Hello Vishal</>
        )}
      </div>
    </div>
  );
}
