import { useSelector } from "react-redux";
import { NavLink, useLoaderData } from "react-router-dom";

export default function ConcertPage() {
  const concertId = useLoaderData();
  const concert = useSelector((state) =>
    state.concerts.concerts.find((concert) => concert.id === concertId)
  );
  if (!concert) {
    return (
      <section>
        <h2>Concert not found!</h2>
        <NavLink to="/concerts">View Concerts</NavLink>
      </section>
    );
  }
  return (
    <section>
      <article>
        <h2>{concert.name}</h2>
      </article>
      <NavLink to="/concerts">View Concerts</NavLink>
    </section>
  );
}
