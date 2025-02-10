import { MoneyInput } from "@/_components/money-input";
import { Button } from "@/_components/ui/button";
import { Calendar } from "@/_components/ui/calendar";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/_components/ui/form";
import { Input } from "@/_components/ui/input";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/_components/ui/popover";
import { cn } from "@/_lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { CalendarIcon } from "lucide-react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import type { InvoiceProps } from "./register-anticipation-form";
import { ptBR } from "date-fns/locale";
import { formatDate } from "@/_utils/formatDate";

const formSchema = z.object({
  number: z.string().min(1, {
    message: "O número da nota é obrigatório",
  }),
  grossAmount: z.number({
    required_error: "O valor total é obrigatório.",
  }),
  dueDate: z.date().refine(
    (date) => {
      const today = new Date();
      today.setHours(23, 59, 59, 999); // Definindo o final do dia de hoje

      // A data fornecida deve ser posterior ao final do dia de hoje
      return date > today;
    },
    {
      message:
        "A data deve ser maior que o dia de hoje (apenas datas futuras são permitidas).",
    }
  ),
});

type FormSchema = z.infer<typeof formSchema>;

const tomorrow = new Date();
tomorrow.setDate(tomorrow.getDate() + 1);
tomorrow.setHours(0, 0, 0, 0); // Opcional: Zerar a hora para 00:00:00

interface AddInvoiceFormProps {
  handleAddInvoice: (invoice: InvoiceProps) => void;
  isTotalExceeded: boolean;
}

export function AddInvoiceForm({
  handleAddInvoice,
  isTotalExceeded,
}: AddInvoiceFormProps) {
  const form = useForm<FormSchema>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      number: "",
      grossAmount: 0,
      dueDate: tomorrow,
    },
  });

  async function onSubmit(data: FormSchema) {
    const { number, grossAmount, dueDate } = data;

    handleAddInvoice({
      number,
      grossAmount,
      dueDate,
    });

    form.reset();
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="grid grid-cols-2 gap-6"
      >
        <FormField
          control={form.control}
          name="number"
          render={({ field }) => (
            <FormItem className="col-span-2">
              <FormLabel className="font-bold">Número</FormLabel>
              <FormControl>
                <Input placeholder="Informe o número da nota" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="grossAmount"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="font-bold">Valor total</FormLabel>
              <FormControl>
                <MoneyInput
                  placeholder="Informe o valor total em R$"
                  value={field.value}
                  onValueChange={({ floatValue }) => field.onChange(floatValue)}
                  onBlur={field.onBlur}
                  disabled={field.disabled}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="dueDate"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="font-bold">Data de vencimento</FormLabel>
              <Popover>
                <PopoverTrigger asChild>
                  <FormControl>
                    <Button
                      variant={"outline"}
                      className={cn(
                        "w-full pl-3 text-left font-normal",
                        !field.value && "text-muted-foreground"
                      )}
                    >
                      {field.value ? (
                        formatDate(field.value)
                      ) : (
                        <span>Selecione uma data</span>
                      )}
                      <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                    </Button>
                  </FormControl>
                </PopoverTrigger>
                <PopoverContent className="w-auto p-0" align="start">
                  <Calendar
                    mode="single"
                    selected={field.value}
                    onSelect={field.onChange}
                    disabled={(date) =>
                      date <= new Date() || date < new Date("1900-01-01")
                    }
                    initialFocus
                    locale={ptBR}
                  />
                </PopoverContent>
              </Popover>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button
          type="submit"
          className="font-medium w-full col-span-2"
          disabled={form.formState.isSubmitting || isTotalExceeded}
        >
          Adicionar nota
        </Button>
      </form>
    </Form>
  );
}
