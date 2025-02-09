"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/_components/ui/form";
import { useForm } from "react-hook-form";

import { z } from "zod";
import { Button } from "@/_components/ui/button";
import { Input } from "@/_components/ui/input";
import { ArrowRightIcon } from "lucide-react";
import { useRouter } from "next/navigation";
import { Card, CardContent, CardFooter } from "@/_components/ui/card";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/_components/ui/select";
import { MoneyInput } from "@/_components/money-input";
import { validateCNPJ } from "@/_utils/validateCNPJ";
import { CNPJInput } from "@/_components/cnpj-input";
import { toast } from "sonner";

const formSchema = z.object({
  name: z
    .string()
    .regex(/^[A-Za-z0-9À-ÿáéíóúãõâêîôûàèìòùäëïöüçÇ-\s]+$/, {
      message:
        "O nome de usuário pode conter apenas letras, números, hífens e espaços.",
    })
    .min(2, {
      message: "O nome da empresa é obrigatório.",
    })
    .transform((val) => val.trim()),
  cnpj: z
    .string()
    .min(14, {
      message: "O CNPJ é obrigatório",
    })
    .max(18, "O CNPJ é inválido")
    .refine((cnpj) => validateCNPJ(cnpj), "O CNPJ é inválido"),
  monthlyRevenue: z.number().min(1, {
    message: "O faturamento mensal é obrigatório.",
  }),
  industry: z.string().min(1, {
    message: "O ramo da empresa é obrigatório.",
  }),
});

type FormSchema = z.infer<typeof formSchema>;

export function RegisterUserForm() {
  const form = useForm<FormSchema>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      cnpj: "",
      monthlyRevenue: 0,
      industry: "",
    },
  });

  const router = useRouter();

  async function onSubmit(data: FormSchema) {
    const { name, cnpj, monthlyRevenue, industry } = data;

    const response = await fetch("/api/auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name,
        cnpj: cnpj.replace(/\D/g, ""),
        monthlyRevenue,
        industryId: +industry,
      }),
    });

    if (!response.ok) {
      const data = await response.json();
      toast.error(data.errorMessages);
      return;
    }

    router.push(`/`);
  }

  return (
    <Card className="min-w-[600px]">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <CardContent className="mt-4 grid grid-cols-2 gap-4">
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => (
                <FormItem className="col-span-2">
                  <FormLabel className="font-bold">Empresa</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Informe o nome da sua empresa"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="cnpj"
              render={({ field }) => (
                <FormItem className="col-span-2">
                  <FormLabel className="font-bold">CNPJ</FormLabel>
                  <FormControl>
                    <CNPJInput
                      format={""}
                      {...field}
                      onValueChange={(values) => field.onChange(values.value)}
                      placeholder="Informe seu CNPJ"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="monthlyRevenue"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="font-bold">
                    Faturamento mensal
                  </FormLabel>
                  <FormControl>
                    <MoneyInput
                      placeholder="Informe seu faturamento em R$"
                      value={field.value}
                      onValueChange={({ floatValue }) =>
                        field.onChange(floatValue)
                      }
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
              name="industry"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="font-bold">Ramo</FormLabel>
                  <FormControl>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}
                    >
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Selecione o ramo da sua empresa" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="1">Serviços</SelectItem>
                        <SelectItem value="2">Produtos</SelectItem>
                      </SelectContent>
                    </Select>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </CardContent>
          <CardFooter className="flex flex-col gap-4">
            <Button
              type="submit"
              className="font-medium w-full"
              disabled={form.formState.isSubmitting}
            >
              Comece agora
              <ArrowRightIcon />
            </Button>
            <div className="text-center text-sm">
              Já tem uma conta?{" "}
              <a
                href="/login"
                className="text-green-500 underline underline-offset-4 hover:text-green-700"
              >
                Faça o login
              </a>
            </div>
          </CardFooter>
        </form>
      </Form>
    </Card>
  );
}
