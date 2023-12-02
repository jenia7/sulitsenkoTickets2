import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchUserOrders } from "./orderedConcertsSlice";

export default function OrdersWidget() {
  let claims = useSelector((state) => state.user.claims);
  let dispatch = useDispatch();
  let status = useSelector((state) => state.orders.status);
  let errMessage = useSelector((state) => state.orders.errMessage);
  let items = useSelector((state) => state.orders.items);
  let cartStatus = useSelector((state) => state.cart.status);

  useEffect(() => {
    if (cartStatus === "idle" && claims) {
      let sub = claims.find((c) => c.type === "sub").value;
      dispatch(fetchUserOrders(sub));
    }
  }, [claims, cartStatus]);

  let content;
  switch (status) {
    case "succeeded":
      let total = items.length;
      content = `You ordered ${total} tickets`;
      break;
    case "failed":
      content = errMessage;
      break;
    case "loading":
      content = "Loading...";
      break;
    default:
      break;
  }

  return <div>{content}</div>;
}
