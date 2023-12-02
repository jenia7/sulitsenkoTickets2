import { Route, createBrowserRouter, createRoutesFromElements } from "react-router-dom";
import ErrorPage from "./components/ErrorPage";
import App from "./App";

import Map from "./components/Map";
import SearchingPage from "./components/SearchingPage";
import ProfilePage from "./components/ProfilePage";
import ConcertPage from "./features/concerts/ConcertPage";
import Cart from "./features/cart/Cart";
import CreateConcert from "./components/CreateConcert";
import OrdersPage from "./features/ordered/OrdersPage";

function loader({ params }) {
  const concertId = parseInt(params.concertId);
  return concertId;
}

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />} errorElement={<ErrorPage />}>
      <Route errorElement={<ErrorPage />}>
        <Route path="concerts" element={<SearchingPage />} />
        <Route path="profile" element={<ProfilePage />} />
        <Route path="concerts/:concertId" element={<ConcertPage />} loader={loader} />
        <Route path="cart" element={<Cart />} />
        <Route path="create" element={<CreateConcert />} />
        <Route path="orders" element={<OrdersPage />} />
      </Route>
    </Route>
  )
);

export default router;
