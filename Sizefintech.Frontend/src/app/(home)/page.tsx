import { getAllAnticipations } from "./_actions/get-all-anticipations";
import { getLoggedUser } from "./_actions/get-user";
import { anticipationColumns } from "./_components/columns";
import { RegisterAnticipationForm } from "./_components/register-anticipation-form";
import { TableAnticipations } from "./_components/table-anticipations";

export default async function HomePage() {
  const loggedUser = await getLoggedUser();
  if (!loggedUser.success) {
    return;
  }

  const responseAnticipations = await getAllAnticipations();
  const data = await responseAnticipations.json();
  const anticipations = responseAnticipations.ok ? data.anticipations : [];

  // capturar erros e exibir no client side

  return (
    <div className="grid min-h-svh grid-cols-[1fr_2fr] grid-rows-1 items-center justify-center gap-6 bg-muted p-6 md:p-10">
      <div className="flex items-center justify-center h-full">
        <RegisterAnticipationForm
          limit={loggedUser.data ? loggedUser.data.limit : 0}
        />
      </div>
      <div className="flex items-center justify-center h-full">
        <TableAnticipations
          columns={anticipationColumns}
          data={JSON.parse(JSON.stringify(anticipations))}
        />
      </div>
    </div>
  );
}
