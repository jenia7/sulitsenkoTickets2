import { useDispatch, useSelector } from "react-redux";
import { removeItem, removeAll, purchase, clear } from "./cartSlice";
import { NavLink } from "react-router-dom";

export default function Cart() {
  const concertsInCart = useSelector((state) => state.cart.items);
  const len = useSelector((state) => state.cart.items.length);
  const status = useSelector((state) => state.cart.status);
  const error = useSelector((state) => state.cart.error);
  const dispatch = useDispatch();
  const onRemoveAllClicked = () => {
    dispatch(removeAll());
  };
  const onRemoveClicked = (id) => {
    dispatch(removeItem({ id }));
  };
  const onPurchaseClicked = async () => {
    dispatch(purchase());
  };
  const renderedConcerts = concertsInCart.map((c) => (
    <div>
      <article key={c.id}>
        <h3>{c.name}</h3>
      </article>
      <button onClick={() => onRemoveClicked(c.id)}>Remove</button>
    </div>
  ));

  let content;
  if (len === 0) {
    content = <div>Your cart is empty</div>;
  } else {
    content = (
      <>
        <div className="cartItems">{renderedConcerts}</div>
        <button onClick={onRemoveAllClicked}>Remove all</button>
        <button onClick={onPurchaseClicked}>Purchase</button>
        <NavLink to="/concerts">View Concerts</NavLink>
      </>
    );
  }
  if (status === "loading") {
    content = <div>Loading...</div>;
  } else if (status === "failed") {
    content = <div>{error}</div>;
  }

  return (
    <section>
      <h2>Cart</h2>
      {content}
    </section>
  );
}
