"use client";

import { formatDate } from "@/_utils/formatDate";
import { ViewAnticipationButton } from "./view-anticipation-button";
import { formatCurrency } from "@/_utils/formatCurrency";
import { ColumnDef } from "@tanstack/react-table";

export type ShortAnticipation = {
  id: number;
  netTotal: number;
  grossTotal: number;
  invoiceCount: number;
  createdAt: Date;
};

export const anticipationColumns: ColumnDef<ShortAnticipation>[] = [
  {
    accessorKey: "id",
    header: () => null, // Oculta no header
    cell: () => null, // Oculta na célula
  },
  {
    accessorKey: "netTotal",
    header: () => <div className="text-center">Total líquido</div>,
    cell: ({ row }) => (
      <div className="text-right">
        {formatCurrency(row.getValue("netTotal"))}
      </div>
    ),
  },
  {
    accessorKey: "grossTotal",
    header: () => <div className="text-center">Total bruto</div>,
    cell: ({ row }) => (
      <div className="text-right">
        {formatCurrency(row.getValue("grossTotal"))}
      </div>
    ),
  },
  {
    accessorKey: "invoiceCount",
    header: () => <div className="text-right">Qtd. de notas</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue("invoiceCount")}</div>
    ),
  },
  {
    accessorKey: "createdAt",
    header: () => <div className="text-center">Criado em</div>,
    cell: ({ row }) => (
      <div className="text-center">{formatDate(row.getValue("createdAt"))}</div>
    ),
  },
  {
    id: "actions",
    enableHiding: false,
    cell: ({ row }) => {
      return (
        <div className="flex items-center justify-center space-x-6">
          <ViewAnticipationButton anticipationId={row.getValue("id")} />
        </div>
      );
    },
  },
];
