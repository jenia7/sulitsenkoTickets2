import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { useDispatch } from "react-redux";
import { fetchUserOrders } from "../ordered/orderedConcertsSlice";

let initialState = {
  claims: null,
  status: "idle",
  error: null,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    addClaims(state, action) {
      state.claims = action.payload;
    },
  },
  extraReducers(builder) {
    builder
      .addCase(fetchUserClaimsAndOrders.pending, (state, action) => {
        state.status = "loading";
      })
      .addCase(fetchUserClaimsAndOrders.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.claims = action.payload;
        state.error = null;
      })
      .addCase(fetchUserClaimsAndOrders.rejected, (state, action) => {
        state.claims = null;
        state.status = "failed";
        state.error = action.error.message;
      });
  },
});
export const { addClaims } = userSlice.actions;
export default userSlice.reducer;

export const fetchUserClaimsAndOrders = createAsyncThunk("user/fetchClaims", async () => {
  const resp = await axios.get("bff/user", { headers: { "X-CSRF": 1 } });

  return resp.data;
});
