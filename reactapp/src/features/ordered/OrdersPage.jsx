import { useSelector } from "react-redux";

export default function OrdersPage() {
  let status = useSelector((state) => state.orders.status);
  let errMessage = useSelector((state) => state.orders.errMessage);
  let items = useSelector((state) => state.orders.items);

  let content;
  switch (status) {
    case "succeeded":
      let mapped = items.map((i) => <article key={i.id}>Name: {i.name}</article>);
      if (!mapped.length) {
        mapped = "You didn't order any item!";
      }
      content = <div>{mapped}</div>;
      break;
    case "failed":
      content = <div>{errMessage}</div>;
      break;
    case "loading":
      content = <div>Loading...</div>;
    default:
      break;
  }
  return (
    <section>
      <h2>Ordered tickets</h2>
      {content}
    </section>
  );
}
