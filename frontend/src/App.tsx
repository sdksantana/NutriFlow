import React from "react";
import "react-toastify/dist/ReactToastify.css";
import { BrowserRouter as Router } from "react-router-dom";
import { AppRoutes } from "./routes/router";
import { ToastContainer } from "react-toastify";

export default function App() {
  return (
    <Router>
      <AppRoutes />
      <ToastContainer position="bottom-right" autoClose={3000} />
    </Router>
  );
}
