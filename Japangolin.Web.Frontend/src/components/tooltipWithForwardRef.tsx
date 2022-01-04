import { Tooltip, TooltipProps } from "@mui/material";
import React from "react";

/*
using MUI <Tooltip> with custom child component shows a console error:
"Failed prop type: Invalid prop `children` supplied to `ForwardRef(Tooltip)`. Expected an element that can hold a ref."
----------
<Tooltip> expects a child that hold a ref, which is limited to things like:
- any MUI component
- DOM components (e.g. <div>, <button>)
- React.forwardRef components
----------
below is an example of forwarding the ref to a child component
usage example: <TooltipWithForwardRef> <GradientIconButton /> </TooltipWithForwardRef>
----------
however, doesn't seem to be needed generally, but keeping for future reference
simplest approach is to just wrap the custom component in something that can already hold a ref
usage example: <Tooltip> <Box> <GradientIconButton /> </Box> </Tooltip>
 */

function TooltipWithForwardRef(props: TooltipProps) {
  const ComponentWithForwardRef = ForwardRefWrapper(props.children);

  return (
    <Tooltip {...props}>
      <ComponentWithForwardRef />
    </Tooltip>
  );
}

const ForwardRefWrapper = (component: React.ReactNode) =>
  React.forwardRef((props, ref) => (
    // @ts-ignore
    <div {...props} ref={ref}>
      {component}
    </div>
  ));

export default TooltipWithForwardRef;
