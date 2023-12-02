import { useDispatch, useSelector } from "react-redux";
import { fetchConcerts } from "../features/concerts/concertsSlice";

import Search from "./Search";
import ConcertsList from "../features/concerts/ConcertsList";

export default function SearchingPage() {
  return (
    <>
      <Search />
      <ConcertsList />
    </>
  );
}
