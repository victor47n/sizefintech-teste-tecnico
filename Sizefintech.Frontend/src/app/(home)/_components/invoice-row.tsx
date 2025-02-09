import { formatCurrency } from "@/_utils/formatCurrency";
import { Trash } from "lucide-react";
import { Button } from "@/_components/ui/button";

interface InvoiceRowProps {
  number: string;
  grossAmount: number;
  dueDate: Date;
  handleRemoveInvoice: (number: string) => void;
}

export function InvoiceRow({
  number,
  grossAmount,
  dueDate,
  handleRemoveInvoice,
}: InvoiceRowProps) {
  return (
    <div className="grid grid-cols-[2fr_1fr_1fr_0.5fr] gap-4 items-center justify-center w-full">
      <span className="text-sm text-muted-foreground">{number}</span>
      <span className="text-sm text-muted-foreground">
        {formatCurrency(grossAmount)}
      </span>
      <span className="text-sm text-muted-foreground">
        {dueDate.toLocaleDateString()}
      </span>
      <span className="flex text-sm text-muted-foreground justify-end">
        <Button variant="ghost" onClick={() => handleRemoveInvoice(number)}>
          <Trash className="text-destructive" />
        </Button>
      </span>
    </div>
  );
}
