import React, { forwardRef } from "react";
import { PatternFormat, PatternFormatProps } from "react-number-format";
import { Input, InputProps } from "@/_components/ui/input";

export const CNPJInput = forwardRef(
  (
    props: PatternFormatProps<InputProps>,
    ref: React.ForwardedRef<HTMLInputElement>
  ) => {
    return (
      <PatternFormat
        {...props}
        format="##.###.###/####-##"
        customInput={Input}
        getInputRef={ref}
      />
    );
  }
);

CNPJInput.displayName = "CNPJInput";
