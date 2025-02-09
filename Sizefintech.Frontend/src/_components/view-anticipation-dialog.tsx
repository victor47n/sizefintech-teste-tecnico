"use client";

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

interface AnticipationProps {
  id: number
}

interface ViewAnticipationDialogProps {
  isOpen: boolean;
  anticipation?: AnticipationProps;
  setIsOpen: (isOpen: boolean) => void;
}

export function ViewAnticipationDialog({
  isOpen,
  setIsOpen,
}: ViewAnticipationDialogProps) {


  return (
    <Dialog
      open={isOpen}
      onOpenChange={(open: boolean) => {
        setIsOpen(open);
      }}
    >
      <DialogContent className="flex h-full max-h-[100vh] max-w-full flex-col overflow-hidden lg:max-h-[90vh] lg:max-w-lg 2xl:h-auto">
        <DialogHeader>
          <DialogTitle>
            Antecipação
          </DialogTitle>
          <DialogDescription>Veja sua antecipação com mais detalhes</DialogDescription>
        </DialogHeader>
        
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