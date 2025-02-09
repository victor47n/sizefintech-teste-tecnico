"use client";

import { Button } from "@/_components/ui/button";
// import { useRouter } from "next/navigation";
import { Card, CardContent, CardFooter } from "@/_components/ui/card";
import { toast } from "sonner";
import { formatCurrency } from "@/_utils/formatCurrency";
import { AddInvoiceForm } from "./add-invoice-form";
import { useMemo, useState } from "react";
import VoidImage from "../../../../public/void.svg";
import { Separator } from "@/_components/ui/separator";
import { InvoiceRow } from "./invoice-row";
import { createAnticipation } from "../_actions/create-anticipation";

export interface InvoiceProps {
  number: string;
  grossAmount: number;
  dueDate: Date;
}

export function RegisterAnticipationForm() {
  // const router = useRouter();
  const [invoices, setInvoices] = useState<InvoiceProps[]>([]);
  const totalGrossAmount = useMemo(() => {
    return invoices.reduce((total, invoice) => total + invoice.grossAmount, 0);
  }, [invoices]);
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function onSubmit() {
    setIsSubmitting(true);
    try {
      const response = await createAnticipation({ invoices });

      if (!response.ok) {
        const data = await response.json();
        toast.error(data.errorMessages);
        setIsSubmitting(false);
        return;
      }

      setIsSubmitting(false);
    } catch (error) {
      console.log(error);

      toast.error("Erro ao criar simulação");
      setIsSubmitting(false);
    }
    // router.push(`/`);
  }

  const handleAddInvoice = (invoice: InvoiceProps) => {
    setInvoices([...invoices, invoice]);
  };

  const handleRemoveInvoice = (number: string) => {
    setInvoices(invoices.filter((i) => i.number !== number));
  };
  const limite = 25000;

  return (
    <Card className="min-w-[600px]">
      <CardContent className="mt-4 flex flex-col gap-8 justify-center">
        <div>
          <h1 className="text-5xl font-bold">Cadastre uma simulação agora</h1>
          <span className="">e tenha seu histórico armazenado.</span>
        </div>
        <span className="text-4xl">
          {formatCurrency(totalGrossAmount)} / {formatCurrency(limite)}{" "}
          <span className="block text-sm text-muted-foreground text-left">
            *O seu limite é de {formatCurrency(limite)} por mês.
          </span>
        </span>
        <AddInvoiceForm handleAddInvoice={handleAddInvoice} />
        <Card className="bg-muted">
          {invoices.length == 0 ? (
            <CardContent className="mt-6 flex flex-col gap-4 items-center justify-center">
              <VoidImage className="h-16" />
              <span className="text-sm text-muted-foreground">
                Nenhuma nota inserido por aqui.
              </span>
            </CardContent>
          ) : (
            <CardContent className="mt-6 flex flex-col gap-4 items-center justify-center">
              {invoices.map((invoice, index) => (
                <div
                  key={invoice.number}
                  className="w-full flex flex-col gap-4"
                >
                  <InvoiceRow
                    number={invoice.number}
                    grossAmount={invoice.grossAmount}
                    dueDate={invoice.dueDate}
                    handleRemoveInvoice={handleRemoveInvoice}
                  />
                  {index < invoices.length - 1 && (
                    <Separator className="col-span-4" />
                  )}
                </div>
              ))}
            </CardContent>
          )}
        </Card>
      </CardContent>
      <CardFooter className="flex flex-col gap-4">
        <Button
          onClick={onSubmit}
          className="font-medium w-full"
          disabled={isSubmitting}
        >
          Simular agora
        </Button>
      </CardFooter>
    </Card>
  );
}
