import { useDispatch, useSelector } from "react-redux";
import { fetchUserClaimsAndOrders } from "./userSlice";
import { useEffect } from "react";

export default function UserLogin() {
  let dispatch = useDispatch();
  let claims = useSelector((state) => state.user.claims);
  let status = useSelector((state) => state.user.status);
  let errMessage = useSelector((state) => state.user.errMessage);

  useEffect(() => {
    dispatch(fetchUserClaimsAndOrders());
  }, []);

  let onLoginClicked = () => {
    window.location = "bff/login";
  };
  let onLogoutClicked = () => {
    let logoutUrl = claims.find((c) => c.type === "bff:logout_url").value;
    window.location = logoutUrl;
  };
  let content;
  switch (status) {
    case "idle":
      content = <button onClick={onLoginClicked}>Login</button>;
      break;
    case "failed":
      content = (
        <div>
          {errMessage}
          <button onClick={onLoginClicked}>Login</button>
        </div>
      );
      break;
    case "succeeded":
      content = <button onClick={onLogoutClicked}>Logout</button>;
      break;
  }
  return <section>{content}</section>;
}
