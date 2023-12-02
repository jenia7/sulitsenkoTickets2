import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

const initialState = {
  items: [],
  status: "idle",
  error: null,
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart(state, action) {
      const newId = action.payload.id;
      const concert = state.items.find((c) => c.id === newId);
      if (!concert) {
        state.items.push(action.payload);
      }
    },
    removeAll(state) {
      state.items.length = 0;
    },
    removeItem(state, action) {
      const id = action.payload.id;

      const items = state.items.filter((c) => c.id !== id);
      return { ...state, items };
    },
    clear(state) {
      state.items = [];
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers(builder) {
    builder
      .addCase(purchase.pending, (state) => {
        state.status = "loading";
      })
      .addCase(purchase.fulfilled, (state, action) => {
        state.status = "idle";
        state.items = [];
        state.error = null;
      })
      .addCase(purchase.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      });
  },
});

export default cartSlice.reducer;

export const { addToCart, removeAll, removeItem, clear } = cartSlice.actions;

export const purchase = createAsyncThunk("cart/purchase", async (_, { getState }) => {
  const state = getState();
  let sub = state.user.claims.find((c) => c.type === "sub").value;
  let concertIds = state.cart.items.map((c) => c.id);
  let body = { sub, concertIds };
  const response = await axios.post("api/orders", body, {
    headers: { "X-CSRF": 1 },
  });
  return response.data;
});
