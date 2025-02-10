"use client";

import { Button } from "@/_components/ui/button";
import {
  ViewAnticipationDialog,
  type AnticipationFullDialogProps,
} from "@/_components/view-anticipation-dialog";
import { EyeIcon } from "lucide-react";
import { useEffect, useState } from "react";
import { getAnticipation } from "../_actions/get-anticipation";
import type { AnticipationResponse } from "../_actions/create-anticipation";
import { toast } from "sonner";

interface ViewAnticipationButtonProps {
  anticipationId: number;
}

export function ViewAnticipationButton({
  anticipationId,
}: ViewAnticipationButtonProps) {
  const [dialogIsOpen, setDialogIsOpen] = useState(false);
  const [anticipation, setAnticipation] =
    useState<AnticipationFullDialogProps | null>(null);

  useEffect(() => {
    async function getAnticipationAsync(anticipationId: number) {
      const response = (await getAnticipation(
        anticipationId
      )) as AnticipationResponse;

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

        setAnticipation(responseProcessed);
      }
    }

    if (dialogIsOpen) {
      getAnticipationAsync(anticipationId);
    }
  }, [anticipationId, dialogIsOpen]);

  return (
    <>
      <Button
        variant="ghost"
        size="icon"
        className="text-muted-foreground"
        onClick={() => setDialogIsOpen(true)}
      >
        <EyeIcon />
      </Button>
      <ViewAnticipationDialog
        isOpen={dialogIsOpen}
        setIsOpen={setDialogIsOpen}
        anticipation={anticipation}
      />
    </>
  );
}
