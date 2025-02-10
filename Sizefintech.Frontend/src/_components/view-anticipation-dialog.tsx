"use client";

import { formatDate } from "@/_utils/formatDate";
import { Button } from "./ui/button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "./ui/dialog";
import { Input } from "./ui/input";
import { Label } from "./ui/label";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "./ui/table";
import { formatCurrency } from "@/_utils/formatCurrency";
import { ScrollArea } from "@radix-ui/react-scroll-area";

interface InvoiceProps {
  number: string;
  netAmount: number;
  grossAmount: number;
  dueDate: Date;
}

export interface AnticipationFullDialogProps {
  cnpj: string;
  company: string;
  limit: number;
  netTotal: number;
  grossTotal: number;
  invoices: InvoiceProps[];
  createdAt: Date;
}

interface ViewAnticipationDialogProps {
  isOpen: boolean;
  anticipation?: AnticipationFullDialogProps | null;
  setIsOpen: (isOpen: boolean) => void;
}

export function ViewAnticipationDialog({
  isOpen,
  setIsOpen,
  anticipation,
}: ViewAnticipationDialogProps) {
  return (
    <Dialog
      open={isOpen}
      onOpenChange={(open: boolean) => {
        setIsOpen(open);
      }}
    >
      <DialogContent className="flex h-full max-h-[100vh] max-w-full flex-col overflow-hidden lg:max-h-[90vh] lg:max-w-2xl 2xl:h-auto">
        <DialogHeader>
          <DialogTitle>Antecipação</DialogTitle>
          <DialogDescription>
            Veja sua antecipação com mais detalhes
          </DialogDescription>
        </DialogHeader>

        {anticipation ? (
          <>
            <div className="grid grid-cols-2 gap-6">
              <div className="flex flex-col col-span-2 gap-4">
                <Label htmlFor="company" className="font-bold">
                  Empresa
                </Label>
                <div className="rounded-md border px-3 py-2 text-base md:text-sm h-10">
                  {anticipation?.company}
                </div>
              </div>
              <div className="flex flex-col col-span-2 gap-4">
                <Label htmlFor="cnpj" className="font-bold">
                  CNPJ
                </Label>
                <div className="rounded-md border px-3 py-2 text-base md:text-sm h-10">
                  {anticipation?.cnpj}
                </div>
              </div>
              <div className="flex flex-col gap-4">
                <Label htmlFor="netTotal" className="font-bold">
                  Total líquido
                </Label>
                <div className="rounded-md border px-3 py-2 text-base md:text-sm h-10">
                  {anticipation?.netTotal
                    ? formatCurrency(anticipation.netTotal)
                    : ""}
                </div>
              </div>
              <div className="flex flex-col gap-4">
                <Label htmlFor="grossTotal" className="font-bold">
                  Total bruto
                </Label>
                <div className="rounded-md border px-3 py-2 text-base md:text-sm h-10">
                  {anticipation?.grossTotal
                    ? formatCurrency(anticipation.grossTotal)
                    : ""}
                </div>
              </div>
              <div className="flex flex-col gap-4">
                <Label htmlFor="limit" className="font-bold">
                  Limite
                </Label>
                <Input
                  type="text"
                  name="limit"
                  disabled={true}
                  value={anticipation?.limit}
                />
              </div>
              <div className="flex flex-col gap-4">
                <Label htmlFor="limit" className="font-bold">
                  Criado em
                </Label>
                <div className="rounded-md border px-3 py-2 text-base md:text-sm h-10">
                  {anticipation?.createdAt
                    ? formatDate(anticipation.createdAt)
                    : ""}
                </div>
              </div>
            </div>

            <ScrollArea className="overflow-auto h-40">
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead className="w-[200px]">Nota fiscal</TableHead>
                    <TableHead className="text-right">Valor líquido</TableHead>
                    <TableHead className="text-right">Valor bruto</TableHead>
                    <TableHead className="text-right">
                      Data de vencimento
                    </TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {anticipation?.invoices?.map((invoice) => (
                    <TableRow key={invoice.number}>
                      <TableCell>{invoice.number}</TableCell>
                      <TableCell className="text-right">
                        {formatCurrency(invoice.netAmount)}
                      </TableCell>
                      <TableCell className="text-right">
                        {formatCurrency(invoice.grossAmount)}
                      </TableCell>
                      <TableCell className="text-right">
                        {formatDate(invoice?.dueDate)}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </ScrollArea>
          </>
        ) : (
          <> </>
        )}

        <DialogFooter className="gap-3">
          <DialogClose asChild>
            <Button type="button" variant="outline">
              Fechar
            </Button>
          </DialogClose>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
