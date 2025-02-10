import { TableCell, TableRow } from "../ui/table";
import EditButton from "./edit-button";
import DeleteButton from "./delete-button";

interface RecordTableRowProps {
    domainInput: string;
    passwordInput: string;
    ipInput: string;
}

const RecordTableRow: React.FC<RecordTableRowProps> = ({domainInput, passwordInput, ipInput}) => {
    return(
        <TableRow>
            <TableCell>{domainInput}</TableCell>
            <TableCell>{ipInput}</TableCell>
            <TableCell className="flex justify-center"><EditButton domain={domainInput} password={passwordInput}></EditButton></TableCell>
            <TableCell><DeleteButton domain={domainInput}></DeleteButton></TableCell>
        </TableRow>
    )
};

export default RecordTableRow;