import { format } from "date-fns";
import { ptBR } from "date-fns/locale";

export function formatDate(date: Date, dateFormat = "PPP") {
  return format(date, dateFormat, { locale: ptBR });
}
