import { SortMeta } from 'primeng/components/common/sortmeta';

export interface ISortEvent {
    data?: any[];
    mode?: string;
    field?: string;
    order?: number;
    multiSortMeta?: SortMeta[];
}
