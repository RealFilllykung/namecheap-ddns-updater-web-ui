import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table";
import DeleteButton from "./delete-button";
import SaveButton from "./save-button";

export default function RecordTable()
{
    const recordModelList = [
        {
            domain: "test.domain.com",
            password: "12345678",
            ip: "123.123.123.123"
        },
        {
            domain: "test2.domain.com",
            password: "87654321",
            ip: "111.111.111.111"
        }
    ]

    return(
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHead>Domain</TableHead>
                    <TableHead>Password</TableHead>
                    <TableHead>IP</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {recordModelList.map((recordModel) => (
                    <TableRow key={recordModel.domain}>
                        <TableCell><Input placeholder="Domain name" value={recordModel.domain}></Input></TableCell>
                        <TableCell><Input placeholder="Password" type="password" value={recordModel.password}></Input></TableCell>
                        <TableCell>{recordModel.ip}</TableCell>
                        <TableCell className="flex justify-center"><SaveButton></SaveButton></TableCell>
                        <TableCell><DeleteButton></DeleteButton></TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}