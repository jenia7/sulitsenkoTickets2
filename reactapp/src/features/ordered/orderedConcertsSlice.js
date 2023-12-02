import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

let initialState = {
  items: null,
  errMessage: null,
  status: "idle",
};

const orderedConcertsSlice = createSlice({
  name: "orderedConcerts",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder
      .addCase(fetchUserOrders.pending, (state, action) => {
        state.status = "loading";
      })
      .addCase(fetchUserOrders.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.items = action.payload;
        state.errMessage = null;
      })
      .addCase(fetchUserOrders.rejected, (state, action) => {
        state.items = null;
        state.status = "failed";
        state.errMessage = action.error.message;
      });
  },
});

export default orderedConcertsSlice.reducer;

export const fetchUserOrders = createAsyncThunk("orderedConcerts/fetch", async (sub) => {
  let resp = await axios.get(`api/orders/${sub}`, { headers: { "X-CSRF": 1 } });
  return resp.data;
});
