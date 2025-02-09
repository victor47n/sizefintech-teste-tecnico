import { anticipationColumns, ShortAnticipation } from "./_components/columns";
import { RegisterAnticipationForm } from "./_components/register-anticipation-form";
import { TableAnticipations } from "./_components/table-anticipations";

const anticipations: ShortAnticipation[] = [
  {
    id: 1,
    netTotal: 1000.50,
    grossTotal: 1200.75,
    invoiceCount: 5,
    createdAt: new Date('2025-02-09T08:00:00')
  },
  {
    id: 2,
    netTotal: 2500.00,
    grossTotal: 3000.00,
    invoiceCount: 10,
    createdAt: new Date('2025-02-10T09:30:00')
  },
  {
    id: 3,
    netTotal: 1500.25,
    grossTotal: 1800.30,
    invoiceCount: 7,
    createdAt: new Date('2025-02-11T10:00:00')
  }
];

export default function HomePage() {
  return (
    <div className="grid min-h-svh grid-cols-[1fr_2fr] grid-rows-1 items-center justify-center gap-6 bg-muted p-6 md:p-10">
      <RegisterAnticipationForm />
      <div className="items-center justify-center h-full">
        <TableAnticipations columns={anticipationColumns} data={anticipations} />
      </div>
    </div>
  );
}
