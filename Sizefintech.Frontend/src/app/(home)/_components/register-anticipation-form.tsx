"use client";

import { Button } from "@/_components/ui/button";
import { Card, CardContent, CardFooter } from "@/_components/ui/card";
import { toast } from "sonner";
import { formatCurrency } from "@/_utils/formatCurrency";
import { AddInvoiceForm } from "./add-invoice-form";
import { useMemo, useState } from "react";
import VoidImage from "../../../../public/void.svg";
import { Separator } from "@/_components/ui/separator";
import { InvoiceRow } from "./invoice-row";
import {
  ViewAnticipationDialog,
  type AnticipationFullDialogProps,
} from "@/_components/view-anticipation-dialog";
import {
  createAnticipation,
  type AnticipationResponse,
} from "../_actions/create-anticipation";
import { ScrollArea } from "@radix-ui/react-scroll-area";
import { ScrollBar } from "@/_components/ui/scroll-area";

export interface InvoiceProps {
  number: string;
  grossAmount: number;
  dueDate: Date;
}

export interface AnticipationFullProps {
  cnpj: string;
  company: string;
  limit: number;
  netTotal: number;
  grossTotal: number;
  invoices: InvoiceProps[];
  createdAt: Date;
}

interface RegisterAnticipationFormProps {
  limit: number;
}

export function RegisterAnticipationForm({
  limit,
}: RegisterAnticipationFormProps) {
  const [invoices, setInvoices] = useState<InvoiceProps[]>([]);
  const totalGrossAmount = useMemo(() => {
    return invoices.reduce((total, invoice) => total + invoice.grossAmount, 0);
  }, [invoices]);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isTotalExceeded, setIsTotalExceeded] = useState(false);
  const [anticipationRegistered, setAnticipationRegistered] = useState<
    AnticipationFullDialogProps | null | undefined
  >(null);
  const [dialogIsOpen, setDialogIsOpen] = useState(false);

  async function onSubmit() {
    setIsSubmitting(true);

    try {
      const invoicesWithFormattedDates = invoices.map((invoice) => ({
        ...invoice,
        dueDate: invoice.dueDate.toISOString(), // Convertendo Date para string
      }));

      const response = (await createAnticipation({
        invoices: invoicesWithFormattedDates,
      })) as AnticipationResponse;

      if (!response.success) {
        toast.error(response.errorMessages);
        return;
      }

      if (response.data) {
        const responseProcessed = {
          ...response.data,
          createdAt: new Date(response.data?.createdAt),
          invoices: response.data?.invoices.map((invoice) => ({
            ...invoice,
            dueDate: new Date(invoice.dueDate),
          })),
        };

        handleRemoveAllInvoices();
        setAnticipationRegistered(responseProcessed);
      }

      toast.success("Antecipação cadastrada com sucesso");
      setDialogIsOpen(true);
    } catch {
      toast.error("Erro desconhecido");
    } finally {
      setIsSubmitting(false);
    }
  }

  useMemo(() => {
    if (totalGrossAmount > limit) {
      setIsTotalExceeded(true);
    } else {
      setIsTotalExceeded(false);
    }
  }, [limit, totalGrossAmount]);

  const handleAddInvoice = (invoice: InvoiceProps) => {
    setInvoices([...invoices, invoice]);
  };

  const handleRemoveInvoice = (number: string) => {
    setInvoices(invoices.filter((i) => i.number !== number));
  };

  const handleRemoveAllInvoices = () => {
    setInvoices([]);
  };

  return (
    <>
      <Card className="min-w-[600px] h-full">
        <CardContent className="mt-4 flex flex-col gap-8 justify-center">
          <div>
            <h1 className="text-5xl font-bold">Cadastre uma simulação agora</h1>
            <span className="">e tenha seu histórico armazenado.</span>
          </div>
          <span className="text-4xl">
            <span className={isTotalExceeded ? "text-destructive" : ""}>
              {formatCurrency(totalGrossAmount)}
            </span>{" "}
            / {formatCurrency(limit)}{" "}
            <span className="block text-sm text-muted-foreground text-left">
              *O seu limite é de {formatCurrency(limit)} por mês.
            </span>
          </span>
          <AddInvoiceForm
            handleAddInvoice={handleAddInvoice}
            isTotalExceeded={isTotalExceeded}
          />
          <Card className="bg-muted">
            {invoices.length == 0 ? (
              <CardContent className="mt-6 flex flex-col gap-4 items-center justify-center">
                <VoidImage className="h-16" />
                <span className="text-sm text-muted-foreground">
                  Nenhuma nota inserido por aqui.
                </span>
              </CardContent>
            ) : (
              <ScrollArea className="overflow-auto">
                <CardContent className="mt-6 flex flex-col gap-4 items-center justify-start h-44">
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
                <ScrollBar orientation="vertical" />
              </ScrollArea>
            )}
          </Card>
        </CardContent>
        <CardFooter className="flex flex-col gap-4">
          <Button
            onClick={onSubmit}
            className="font-medium w-full"
            disabled={isSubmitting || isTotalExceeded}
          >
            Simular agora
          </Button>
        </CardFooter>
      </Card>
      <ViewAnticipationDialog
        isOpen={dialogIsOpen}
        setIsOpen={setDialogIsOpen}
        anticipation={anticipationRegistered}
      />
    </>
  );
}
