"use client"

import { Button } from "@/_components/ui/button";
import {ViewAnticipationDialog} from "@/_components/view-anticipation-dialog";
import { EyeIcon } from "lucide-react";
import { useState } from "react";

interface ViewAnticipationButtonProps {
  anticipationId: number;
}

export function ViewAnticipationButton({
  anticipationId,
}: ViewAnticipationButtonProps) {
  const [dialogIsOpen, setDialogIsOpen] = useState(false);

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
        anticipation={{ id: anticipationId}}
      />
    </>
  );
}