import { Outlet } from "react-router-dom";
import Header from "./components/Header";
import Footer from "./components/Footer";
import NavPanel from "./components/NavPanel";

export default function App() {
  return (
    <>
      <Header>Header</Header>
      <NavPanel />
      <main>{<Outlet />}</main>
      <Footer>Footer</Footer>
    </>
  );
}
