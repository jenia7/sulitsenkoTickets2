import { NavLink } from "react-router-dom";
import { addToCart } from "../cart/cartSlice";
import { useDispatch, useSelector } from "react-redux";

export default function ConcertCard({ concert }) {
  const dispatch = useDispatch();
  const addToCartClicked = () => {
    dispatch(addToCart(concert));
  };
  let ordered = useSelector((state) => state.orders.items);
  let inCartItems = useSelector((state) => state.cart.items);
  let maybeOrdered = false;
  let maybeInCart = false;
  if (ordered) {
    let item = ordered.find((item) => item.name === concert.name);
    if (item) {
      maybeOrdered = true;
    }
  }
  if (inCartItems) {
    let item = inCartItems.find((item) => item.id === concert.id);
    if (item) {
      maybeInCart = true;
    }
  }
  let canClick = maybeInCart || maybeOrdered;
  return (
    <article className="card">
      <h3>{concert.name}</h3>
      <p>Description...</p>
      <NavLink to={`/concerts/${concert.id}`}>View Concert</NavLink>
      <button onClick={addToCartClicked} disabled={canClick}>
        Add to Cart
      </button>
    </article>
  );
}
