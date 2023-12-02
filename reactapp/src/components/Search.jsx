import { useState } from "react";
import { fetchConcerts } from "../features/concerts/concertsSlice";
import { useDispatch, useSelector } from "react-redux";

export default function Search() {
  const dispatch = useDispatch();
  const status = useSelector((state) => state.concerts.status);
  const [pattern, setPattern] = useState("");
  const [filters, setFilters] = useState([false, false, false]);
  const onPatternChange = (e) => setPattern(e.target.value);
  const onSearchButtonClicked = () => {
    const arr = [];
    if (filters[0]) {
      const elem = document.getElementById("classic");
      arr.push(elem.value);
    }
    if (filters[1]) {
      const elem = document.getElementById("openAir");
      arr.push(elem.value);
    }
    if (filters[2]) {
      const elem = document.getElementById("party");
      arr.push(elem.value);
    }
    dispatch(fetchConcerts({ filters: arr, searchPattern: pattern }));
  };
  const onCheckBoxClicked = (current) => {
    setFilters(
      filters.map((elem, index) => {
        if (index === current) {
          return !elem;
        }
        return elem;
      })
    );
  };
  if (status === "loading") {
    return <div>Loading...</div>;
  }
  if (status === "failed") {
    return <div>error during fetching concerts. Try again later...</div>; // TODO handle error: add button to change status
  }
  return (
    <form>
      <div className="search">
        <input
          id="search"
          name="search"
          value={pattern}
          type="search"
          placeholder="Search here..."
          onChange={onPatternChange}
        />
        <button type="button" onClick={onSearchButtonClicked}>
          Search
        </button>
      </div>
      <fieldset>
        <legend>Filters</legend>
        <ul>
          <li>
            <label htmlFor="classic">Classic:</label>
            <input
              id="classic"
              type="checkbox"
              name="type"
              value="classic"
              checked={filters[0]}
              onChange={() => onCheckBoxClicked(0)}
            />
          </li>
          <li>
            <label htmlFor="openAir">OpenAir:</label>
            <input
              id="openAir"
              type="checkbox"
              name="type"
              value="openAir"
              checked={filters[1]}
              onChange={() => onCheckBoxClicked(1)}
            />
          </li>
          <li>
            <label htmlFor="party">Party:</label>
            <input
              id="party"
              type="checkbox"
              name="type"
              value="party"
              checked={filters[2]}
              onChange={() => onCheckBoxClicked(2)}
            />
          </li>
        </ul>
      </fieldset>
    </form>
  );
}
