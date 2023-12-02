import { configureStore } from "@reduxjs/toolkit";
import concertsReducer from "../features/concerts/concertsSlice";
import cartReducer from "../features/cart/cartSlice";
import userReducer from "../features/user/userSlice";
import orderedConcertsReducer from "../features/ordered/orderedConcertsSlice";

export default configureStore({
  reducer: {
    user: userReducer,
    cart: cartReducer,
    concerts: concertsReducer,
    orders: orderedConcertsReducer,
  },
});
