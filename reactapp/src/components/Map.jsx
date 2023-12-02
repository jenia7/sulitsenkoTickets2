import { useRef } from "react";

export default function Map() {
  let map = useRef();
  function toggleClicked() {
    let map = document.getElementById("map");
    if (map.style.display === "block") {
      map.style.display = "none";
    } else {
      map.style.display = "block";
    }
  }
  return (
    <section>
      <h2>Map</h2>
      <button onClick={toggleClicked}>Show/Hide</button>
      <div id="map"></div>
    </section>
  );
}
