import OrdersWidget from "../features/ordered/OrdersWidget";
import UserLogin from "../features/user/UserLogin";

export default function Header() {
  return (
    <header>
      <h1>App for ordering tickets</h1>
      <div>
        <UserLogin />
        <OrdersWidget />
      </div>
    </header>
  );
}
