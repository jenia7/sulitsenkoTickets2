import { useSelector } from "react-redux";

export default function ProfilePage() {
  let claims = useSelector((state) => state.user.claims);
  let content;
  if (claims) {
    let name = claims.find((c) => c.type === "name");
    content = `name: ${name.value}`;
  } else {
    content = "Please, log in";
  }
  return (
    <section>
      <h2>Profile</h2>
      <div>{content}</div>
    </section>
  );
}
