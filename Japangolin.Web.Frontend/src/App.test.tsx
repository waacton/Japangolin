import React from "react";
import { render, screen } from "@testing-library/react";
import App from "./App";

test("renders japangolin app", () => {
  render(<App />);
  const headerElement = screen.getByText(/wacton.japangolin/i);
  expect(headerElement).toBeInTheDocument();
});
