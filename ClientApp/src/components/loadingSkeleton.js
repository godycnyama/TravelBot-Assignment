import React from 'react';
import Skeleton from "react-loading-skeleton";

const LoadingSkeleton = () => {
    return (
        <tbody>
            {Array(15).fill().map((item, index) => {
                return (
                  <tr key={index}>
                    <td>
                        <Skeleton height={36} />
                    </td>
                    <td>
                        <Skeleton height={36} />
                    </td>
                    <td>
                        <Skeleton height={36} />
                    </td>
                    <td>
                        <Skeleton height={36} />
                    </td>
                    <td>
                        <Skeleton height={36} />
                    </td>
                    </tr>
                )
            })}
        </tbody>
    )
}

export default LoadingSkeleton;