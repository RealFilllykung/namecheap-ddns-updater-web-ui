import { useEffect, useState } from "react";
import { Table, TableBody, TableHead, TableHeader, TableRow } from "../ui/table";
import RecordTableRow from "./record-table-row";

const RecordTable = () =>
{
    const [getRecordModelList, setRecordModelList] = useState([])

    const getAllRecords = async () => {
        const response = await fetch(process.env.NEXT_PUBLIC_RECORD_API_URL!, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET',
                'Access-Control-Allow-Headers': 'Content-Type',
            }
        })
        const content = await response.json()
        return content
    }

    useEffect(() => {
        getAllRecords().then(content => {
            setRecordModelList(content)
        })
    },[])

    if(getRecordModelList.length == 0) return(<div>There is no domain record maintained yet</div>);
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
                    <RecordTableRow key={recordModel.domain} domainInput={recordModel.domain} passwordInput={recordModel.password} ipInput={recordModel.ip}></RecordTableRow>
                ))}
            </TableBody>
        </Table>
    );
};

export default RecordTable;