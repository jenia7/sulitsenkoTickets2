import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

const initialState = {
  concerts: [],
  status: "idle",
  error: null,
};

const concertsSlice = createSlice({
  name: "concerts",
  initialState,
  reducers: {
    reducerName(state, action) {},
  },
  extraReducers(builder) {
    builder
      .addCase(fetchConcerts.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchConcerts.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.concerts = action.payload;
      })
      .addCase(fetchConcerts.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      });
  },
});

export default concertsSlice.reducer;

export const fetchConcerts = createAsyncThunk(
  "concerts/fetch",
  async ({ filters, searchPattern }) => {
    let params = { filters, searchPattern };

    const response = await axios.get("api/concerts", {
      headers: { "X-CSRF": 1 },
      params,
      paramsSerializer(params) {
        let str =
          params.filters.map((f) => `filters=${f}`).join("&") +
          `&pattern=${searchPattern}`;
        //?status=NOT_SOLVED&status=SOLVED&status=WRONG&difficulty=EASY&difficulty=HARD&difficulty=MEDIUM
        return str;
      },
    });
    return response.data;
  }
);
