import { useSelector } from "react-redux";
import ConcertCard from "./ConcertCard";

export default function ConcertsList() {
  const concerts = useSelector((state) => state.concerts.concerts);
  const list = concerts.map((c) => <li key={c.id}>{<ConcertCard concert={c} />}</li>);
  return <ul className="cardsWrapper">{list}</ul>;
}
