import { useEffect, useState } from "react";
import { Table, TableBody, TableHead, TableHeader, TableRow } from "../ui/table";
import RecordTableRow from "./record-table-row";
import RecordModel from "@/models/RecordModel";
import { Alert, AlertDescription, AlertTitle } from "../ui/alert";
import { Terminal } from "lucide-react";

const getAllRecords = async () => {
    const response = await fetch(process.env.NEXT_PUBLIC_RECORD_API_URL!)
    const content = await response.json()
    return content
}

const RecordTable = () =>
{
    const [getRecordModelList, setRecordModelList] = useState<RecordModel[]>([])

    useEffect(() => {
        getAllRecords().then(content => {
            setRecordModelList(content)
        })
    },[])

    if(getRecordModelList.length == 0) return(
        <div className="m-2">
            <Alert>
                <Terminal className="h-4 w-4" />
                <AlertTitle>There is no DDNS record yet...</AlertTitle>
                <AlertDescription>
                    Click create new record to add your first domain record.
                </AlertDescription>
            </Alert>
        </div>

    );
    else return(
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHead>Domain</TableHead>
                    <TableHead>IP</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {getRecordModelList.map((recordModel) => (
                    <RecordTableRow key={recordModel.domain} domainInput={recordModel.domain} ipInput={recordModel.ip} passwordInput={recordModel.password}></RecordTableRow>
                ))}
            </TableBody>
        </Table>
    );
};

export default RecordTable;