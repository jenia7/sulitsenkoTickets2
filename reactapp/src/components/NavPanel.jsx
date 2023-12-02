import { useSelector } from "react-redux";
import { NavLink } from "react-router-dom";

export default function NavPanel() {
  const itemsInCart = useSelector((state) => state.cart.items.length);
  let claims = useSelector((state) => state.user.claims);
  let contentForAdmin;
  if (claims) {
    let name = claims.find((c) => c.type === "name").value;
    if (name === "admin") {
      contentForAdmin = (
        <li>
          <NavLink to="/create">Create concert</NavLink>
        </li>
      );
    }
  }
  const rendered = "(" + itemsInCart + ")";
  return (
    <nav>
      <ul>
        <li>
          <NavLink to="/">Home</NavLink>
        </li>
        <li>
          <NavLink to="/concerts">Concerts</NavLink>
        </li>
        {contentForAdmin}
        <li>
          <NavLink to="/cart">Cart{itemsInCart === 0 ? null : rendered}</NavLink>
        </li>
        <li>
          <NavLink to="/profile">Profile</NavLink>
        </li>
        <li>
          <NavLink to="/orders">Orders</NavLink>
        </li>
      </ul>
    </nav>
  );
}
